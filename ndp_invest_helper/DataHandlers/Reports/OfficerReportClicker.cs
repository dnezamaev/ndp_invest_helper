using System;
using System.Collections.Generic;

using WindowsInput;
using WindowsInput.Native;
using System.Threading;

using ndp_invest_helper.DataHandlers;

namespace ndp_invest_helper.ReportHandlers
{
    public enum AutoClickerStatus { NotStarted, FocusingWindow, Clicking, Finished }

    public enum AutoClickerTask { Shares, Others }

    public class ClickerCancelException : Exception { }

    /// <summary>
    /// Авто кликер для заполнения декларации госслужащих.
    /// </summary>
    public class OfficerReportClicker
    {
        /// <summary>
        /// Current status.
        /// </summary>
        public AutoClickerStatus Status { get; private set; } 
            = AutoClickerStatus.NotStarted;

        /// <summary>
        /// Current task.
        /// </summary>
        public AutoClickerTask Task { get; private set; }

        /// <summary>
        /// Last handled report.
        /// </summary>
        public OfficerReport Report { get; private set; }

        /// <summary>
        /// Time before clicking start,
        /// so user able focus clicked program.
        /// </summary>
        private int startDelaySec { get; set; }

        /// <summary>
        /// Time between input emulator action in milisecond.
        /// </summary>
        private int clickDelayMs { get; set; }

        /// <summary>
        /// Keyboard/mouse emulator.
        /// </summary>
        private InputSimulator inputSimulator;

        /// <summary>
        /// Method to report about progress to caller.
        /// </summary>
        Action<int, AutoClickerStatus> progressHandler;

        /// <summary>
        /// Method to check if caller canceled task.
        /// </summary>
        Func<bool> checkCancelHandler;

        /// <summary>
        /// Indexes of issuer types in combobox.
        /// </summary>
        private static Dictionary<IssuerType, int> IssuerTypesCombobox
            = new Dictionary<IssuerType, int>()
        {
            {IssuerType.Public, 7 }
        };

        /// <summary>
        /// Indexes of share types in combobox.
        /// </summary>
        private static Dictionary<ShareType, int> ShareTypesCombobox
            = new Dictionary<ShareType, int>()
        {
            {ShareType.Preferred, 5 },
            {ShareType.Common, 6 }
        };

        public OfficerReportClicker
            (
            int clickDelayMs, 
            int startDelaySec,
            Action<int, AutoClickerStatus> progressHandler,
            Func<bool> checkCancelHandler,
            AutoClickerTask task
            )
        {
            inputSimulator = new InputSimulator();
            this.clickDelayMs = clickDelayMs;
            this.startDelaySec = startDelaySec;
            this.progressHandler = progressHandler;
            this.checkCancelHandler = checkCancelHandler;
            Task = task;
        }

        private void SimulateKey(VirtualKeyCode key)
        {
            if (checkCancelHandler())
                throw new ClickerCancelException();

            inputSimulator.Keyboard.KeyPress(key);
            Thread.Sleep(clickDelayMs);
        }

        private void SimulateKeyCombo(VirtualKeyCode modificator, VirtualKeyCode key)
        {
            if (checkCancelHandler())
                throw new ClickerCancelException();

            inputSimulator.Keyboard.ModifiedKeyStroke(modificator, key);
            Thread.Sleep(clickDelayMs);
        }

        private void SimulateText(string text)
        {
            if (checkCancelHandler())
                throw new ClickerCancelException();

            inputSimulator.Keyboard.TextEntry(text);
            Thread.Sleep(clickDelayMs);
        }

        public void Start(OfficerReport report)
        {
            Report = report;
            Status = AutoClickerStatus.FocusingWindow;

            // give user time to focus window
            for (int i = 0; i < startDelaySec; i++)
            {
                progressHandler(i, AutoClickerStatus.FocusingWindow);

                Thread.Sleep(1000);

                if (checkCancelHandler()) return; // user stopped task
            }

            Status = AutoClickerStatus.Clicking;

            for (int i = 0; i < Report.Shares.Count; i++)
            {
                var share = Report.Shares[i];

                // go to Add button and press it
                SimulateKey(VirtualKeyCode.DOWN);
                SimulateKey(VirtualKeyCode.DOWN); 
                SimulateKey(VirtualKeyCode.RETURN); 

                // issuer type
                if (share.TypeOfIssuer != IssuerType.Unknown)
                {
                    // select issuer type in dropdown list
                    for (int j = 0; j < IssuerTypesCombobox[share.TypeOfIssuer]; j++)
                    {
                        SimulateKey(VirtualKeyCode.DOWN);
                    }

                    // confirm selection
                    SimulateKey(VirtualKeyCode.RETURN);
                }

                // issuer name
                SimulateKey(VirtualKeyCode.TAB);
                SimulateKey(VirtualKeyCode.TAB); 
                SimulateText(share.Issuer);

                // address

                // В справке от ВТБ адреса указаны в произвольном формате.
                // Качественно распарсить их невозможно.
                // Поэтому запишем всю строку в доп. инфу.
                // В качестве региона и города укажем Москву, 
                // т.к. это обязательные поля, иначе кнопка ОК недоступна.
                const string CityStub = "Москва";

                SimulateKey(VirtualKeyCode.TAB);
                SimulateKey(VirtualKeyCode.TAB);
                SimulateKey(VirtualKeyCode.RETURN); // open address form

                SimulateKey(VirtualKeyCode.TAB);
                SimulateText(CityStub);
                SimulateKey(VirtualKeyCode.DOWN);
                SimulateKey(VirtualKeyCode.RETURN); // region textbox

                SimulateKey(VirtualKeyCode.TAB);
                SimulateText(CityStub);
                SimulateKey(VirtualKeyCode.DOWN);
                SimulateKey(VirtualKeyCode.RETURN); // city textbox

                SimulateKey(VirtualKeyCode.TAB);
                SimulateKey(VirtualKeyCode.TAB);
                SimulateKey(VirtualKeyCode.TAB); 
                SimulateKey(VirtualKeyCode.TAB);
                SimulateText(share.AddressFull); // info textbox

                SimulateKey(VirtualKeyCode.ESCAPE);
                SimulateKey(VirtualKeyCode.RETURN); // save address

                // authorized capital
                SimulateText(share.AuthorizedCapital); 
                
                // share type
                SimulateKey(VirtualKeyCode.TAB); 

                if (share.TypeOfShare != ShareType.Unknown)
                {
                    for (int j = 0; j < ShareTypesCombobox[share.TypeOfShare]; j++)
                    {
                        SimulateKey(VirtualKeyCode.DOWN);
                    }

                    SimulateKey(VirtualKeyCode.RETURN);
                }

                // price
                SimulateKey(VirtualKeyCode.TAB);
                SimulateKey(VirtualKeyCode.TAB); 
                SimulateText(share.Price); 

                // count
                SimulateKey(VirtualKeyCode.TAB); 
                SimulateText(share.Count); 

                // reason
                SimulateKey(VirtualKeyCode.TAB);
                SimulateKey(VirtualKeyCode.TAB); 
                SimulateText("Покупка"); 

                // close window and save changes
                SimulateKey(VirtualKeyCode.ESCAPE); 
                SimulateKey(VirtualKeyCode.RETURN); 

                //SimulateKeyCombo(VirtualKeyCode.SHIFT, VirtualKeyCode.TAB);

                progressHandler(i, AutoClickerStatus.Clicking);
            }

            Status = AutoClickerStatus.Finished;
        }
    }
}
