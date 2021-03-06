# TODO list

## Текущие

* GUI. Вкладки редактирования базы и заполнения справки БК добавлять по пункту меню Сервис.
* Парсер отчетов Альфы.
* Сайт
  * Редактор базы через предложения.
  * Просмотр, скачивание базы в формате архива xml файлов.
* Обновить инструкцию пользователя. Добавить видео.
* Фильтры по бумагам, странам, секторам, валютам, типам актива.
* Анализ качества диверсификации. 
  * На наличие всех существующих бумаг, стран, секторов.
  * На указанные пропорции.
  * Итоговая числовая оценка качества по разным методикам.
* Редактор базы данных.
* GUI. Настройка "Показывать изменение" - "С начала", "С предыдущего".
* GUI. Добавить меню с выбором уровней группировки. Меню заполняется динамически на основе количества уровней. Группировка в одном и том же DataGridView.
* Учитывать наличность из отчетов брокера, добавлять в остальную наличность.
* Группировка по регионам и уровню развитости страны.
* GUI. Добавить pie charts.
* Добавить анализ по льготе долгосрочного владения. Какие бумаги, в каком количестве, с какого счета и когда можно продать без уплаты налогов.
* Добавить анализ убытков за последние N лет, для зачета налога по прибыли.
* GUI. Добавить деньги в ListBox со списком бумаг или отдельный ListBox.
* GUI. Настройка. Добавить в вывод по результатам группировки неиспользованные группы.

## Сделано

* GUI. Переделать все контролы на Krypton.

* Редактор базы данных по диверсификации.
* Привести все менеджеры к одному типу. Сделать разбор xml тэгов по критериям универсальным.
* Добавить в базу Id для стран и валют - номера по ISO. Переделать группировку на основе int. Добавить в Sector, Country поля Id.
* GUI. Форма настроек.
* Перенести деньги вкладов и т.п. из cash.xml в файл настроек.
* Автозаполнение справки для госслужащих.
* Добавить класс InvestManager, в котором будут парситься и храниться отчеты, инициализироваться все менеджеры и т.п.
* Ошибка в базе - Securities_EconomySectors_Link -> Part должен быть REAL. Переделать скрипт создания БД и запустить заново импорт.
* Переделать GUI на Krypton.
* Завести файл data\info\currencies.xml для валюты. С названиями, странами и курсами. Перенести курсы из Setting.xml.
* Перенести Settings.xml в Settings.settings проекта. Класс Settings будет оберткой.
* Сделать локальную базу данных Sqlite для удобного обновления.
* Суммирование процентов выделенных строк в таблицах группировки.
* GUI. В списке Состав правой панели добавить проценты - долю бумаги в элементе группы.
* Парсер отчетов Тинькова.
* Каждой бумаге добавить аттрибут name, в котором будет храниться название. Например, SBER и SBERP - Сбербанк акция обыкновенная и Сбербанк акция привилегированная. Чтобы показывать это имя вместо тикера.
* GUI. Добавить лог ошибок и важных сообщений в правую панель.
* Ошибка при отсутствии или недозаполненности бумаги в базе. Кидается исключение, надо заменить на сообщение в логе справа.
* GUI. Добавить справа TabControl, одна вклада - состав группы, вторая - история сделок. По клику показывать результат группировки после этой сделки, навигация.
* GUI. Баг с валютой в итого, дублирует комбобокс, а должна быть валютой бумаги.
* GUI. Переделать Итого на NumericUpDown, чтобы можно было вводить сумму, а количество считалось автоматом исходя из цены. Округление в меньшую сторону с поправкой введенного значения. Increment сделать равным цене.
* GUI. При выборе валюты в комбобоксе пересчитывать цену и итого в ней.
* Баг с TBIO. В отчете в bond_price1 указана цена в долларах. Надо анализировать поле currency_ISO и переводить цену в рубли.
* Перевести наличность из Settings.xml в data/reports/cash.xml
* GUI. Отмена сделки.
* GUI. Настройка в settings.xml. Показывать изменения по сравнению с результатом предыдущей операции или изначальным состоянием портфеля. Важно, если несколько сделок, и нужно посмотреть их накопленный эффект на портфель.
* GUI. После купли, продажи подсвечивать зеленым или красным изменение доли. Можно писать его в той же ячейке типа 75(+5) или отдельном столбце. Лучше в столбце, чтобы можно было упорядочивать отдельно долю и изменение.
* GUI. Настройка - вести журнал операций. В Settings.xml путь к файлу лога. В меню настроек галочка.
* GUI. Таблица DataGridView со всеми группировками и настройками.
* Человеческие названия у групп. Вместо кодов - названия стран, валют, активов и т.п.
* Аргументы командной строки
* Добавить в задании действия buy/sell для просмотра, что будет с результатами группироки при докупке/продаже бумаг. 
* Добавить в settings.xml пути к файлам с инфой по бумагам, странам, секторам, к директории с отчетами.
* Добавить в пакетное задание для группировки аттрибут join: список групп для объединения в одну. Все группы будут сливаться в первую указанную. Несколько отдельных сливаний разделять например чертой |
* Заполнить сектора у недостающих эмитентов.
* Группировка по секторам с уровнями вложенности.
* Добавить список стран с инфой по регионам и уровню развитости экономики.
* Почистить названия эмитентов в issuers.xml.
* UserManual.md
* Обработка ошибок при разборе xml файлов.
* Пакетные задания в task.xml. 
	  * Группировка, в том числе рекурсивная.
	  * Скрывать результаты ниже указанного %.
	  * Удаление ключей из результата. Аттрибут remove_keys в тэге group.
	  * Оставление ключей. Аттрибут keep_only_keys в тэге group.
	  * Сортировка результатов.
	  * Удаление отдельных акций из результата.
	  * Оставление только отдельных акций в результате.

## Отложено

* В issuers.xml у ETF добавить аттрибут index. В нем будет ссылка на описание индекса, за которым следует ETF. Описания хранятся в indexes.xml с теми же параметрами, что у ETF. Это избавит от проблемы с дублированием данных в разных ETF на один индекс.
* Добавить в список стран английские названия.
* Юнит тесты на песочнице.
  * Неверно считается сумма при однократной группировке по странам и валютам.
  * Баг: при многократной группировке на by="share" возникает сбой, сумма улетает в небеса.
  * Проверить вложенную группировку. Вероятно, доли при этом учитываются неправильно. Надо перменожать коэффициент коррекции.

Добавить в задании действие rebalance. Аттрибут type определяет алгоритм балансировки:
  * fix_total - добиться желаемых пропрорций при неизменной общей сумме, полезно для ИИС типа Б с лимитом по сумме.
  * buy_only - разрешена только покупка бумаг.

Подтеги make key="ключ последней группировки" value="желаемое значение в процентах". 

В результате высвечивается сколько надо купить/продать в каждой группе, чтобы получились указанные пропорции. Если указанного ключа нет, то он добавляется. Если указаны не все ключи, то для fix_total их доли будут считаться равными нулю, для buy_only - неизменными.
