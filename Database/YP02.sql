-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Мар 31 2025 г., 18:00
-- Версия сервера: 8.0.30
-- Версия PHP: 8.1.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `YP02`
--

-- --------------------------------------------------------

--
-- Структура таблицы `Auditories`
--

CREATE TABLE `Auditories` (
  `Id` int NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `ShortName` varchar(255) DEFAULT NULL,
  `ResponUser` int DEFAULT NULL,
  `TimeResponUser` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Auditories`
--

INSERT INTO `Auditories` (`Id`, `Name`, `ShortName`, `ResponUser`, `TimeResponUser`) VALUES
(1, 'Аудитория А-422', 'А-422', 3, 2),
(2, 'Аудитория А-424', 'А-424', 2, 1),
(3, 'Аудитория А-418', 'А-418', 1, 1),
(8, 'Нет аудитории', 'Нет', 1, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `Characteristics`
--

CREATE TABLE `Characteristics` (
  `Id` int NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `TypeCharacter` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Characteristics`
--

INSERT INTO `Characteristics` (`Id`, `Name`, `TypeCharacter`) VALUES
(1, 'Цвет', 1),
(2, 'Материал', 1),
(3, 'Стоимость', 2),
(8, 'Размер', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `Developers`
--

CREATE TABLE `Developers` (
  `Id` int NOT NULL,
  `Name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Developers`
--

INSERT INTO `Developers` (`Id`, `Name`) VALUES
(1, 'Backend-разработчик'),
(2, 'Frontend-разработчик');

-- --------------------------------------------------------

--
-- Структура таблицы `Errors`
--

CREATE TABLE `Errors` (
  `Id` int NOT NULL,
  `Message` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Errors`
--

INSERT INTO `Errors` (`Id`, `Message`) VALUES
(1, 'An error occurred while updating the entries. See the inner exception for details.'),
(2, 'An error occurred while updating the entries. See the inner exception for details.'),
(3, 'An error occurred while updating the entries. See the inner exception for details.'),
(4, 'An error occurred while updating the entries. See the inner exception for details.'),
(5, 'An error occurred while updating the entries. See the inner exception for details.'),
(6, 'An error occurred while updating the entries. See the inner exception for details.'),
(7, 'An error occurred while updating the entries. See the inner exception for details.'),
(8, 'An error occurred while updating the entries. See the inner exception for details.'),
(9, 'An error occurred while updating the entries. See the inner exception for details.'),
(10, 'An error occurred while updating the entries. See the inner exception for details.'),
(11, 'An error occurred while updating the entries. See the inner exception for details.'),
(12, 'An error occurred while updating the entries. See the inner exception for details.'),
(13, 'An error occurred while updating the entries. See the inner exception for details.'),
(14, 'An error occurred while updating the entries. See the inner exception for details.'),
(15, 'An error occurred while updating the entries. See the inner exception for details.'),
(16, 'An error occurred while updating the entries. See the inner exception for details.'),
(17, 'An error occurred while updating the entries. See the inner exception for details.'),
(18, 'An error occurred while updating the entries. See the inner exception for details.'),
(19, 'An error occurred while updating the entries. See the inner exception for details.'),
(20, 'An error occurred while updating the entries. See the inner exception for details.'),
(21, 'Не удалось обнаружить компонент обработки изображений, который подходит для завершения данной операции.'),
(22, 'Не удалось обнаружить компонент обработки изображений, который подходит для завершения данной операции.'),
(23, 'Не удалось обнаружить компонент обработки изображений, который подходит для завершения данной операции.'),
(24, 'Не удалось обнаружить компонент обработки изображений, который подходит для завершения данной операции.');

-- --------------------------------------------------------

--
-- Структура таблицы `HistoryInventory`
--

CREATE TABLE `HistoryInventory` (
  `Id` int NOT NULL,
  `OborId` int DEFAULT NULL,
  `IdUser` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `HistoryObor`
--

CREATE TABLE `HistoryObor` (
  `Id` int NOT NULL,
  `IdUserr` int DEFAULT NULL,
  `IdObor` int DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `Comment` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `Inventory`
--

CREATE TABLE `Inventory` (
  `Id` int NOT NULL,
  `StartDate` datetime DEFAULT NULL,
  `EndDate` datetime DEFAULT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `UserId` int DEFAULT NULL,
  `IdOborrud` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Inventory`
--

INSERT INTO `Inventory` (`Id`, `StartDate`, `EndDate`, `Name`, `UserId`, `IdOborrud`) VALUES
(7, '2024-06-01 00:00:00', '2024-06-07 00:00:00', 'Летняя инвентаризация', 2, 2);

-- --------------------------------------------------------

--
-- Структура таблицы `Napravlenie`
--

CREATE TABLE `Napravlenie` (
  `Id` int NOT NULL,
  `Name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Napravlenie`
--

INSERT INTO `Napravlenie` (`Id`, `Name`) VALUES
(1, 'Офис'),
(3, 'Информационные технологии'),
(5, 'WSR2025'),
(7, 'Нет направления');

-- --------------------------------------------------------

--
-- Структура таблицы `NetworkSettings`
--

CREATE TABLE `NetworkSettings` (
  `Id` int NOT NULL,
  `IpAddress` varchar(15) DEFAULT NULL,
  `SubnetMask` varchar(15) DEFAULT NULL,
  `MainGateway` varchar(15) DEFAULT NULL,
  `DNSServer1` varchar(45) DEFAULT NULL,
  `DNSServer2` varchar(45) DEFAULT NULL,
  `DNSServer3` varchar(45) DEFAULT NULL,
  `DNSServer4` varchar(45) DEFAULT NULL,
  `OborudovanieId` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `NetworkSettings`
--

INSERT INTO `NetworkSettings` (`Id`, `IpAddress`, `SubnetMask`, `MainGateway`, `DNSServer1`, `DNSServer2`, `DNSServer3`, `DNSServer4`, `OborudovanieId`) VALUES
(1, '192.168.0.111', '255.255.255.255', '192.168.0.1', '123.123.123.1', '123.123.123.2', '123.123.123.3', '123.123.123.4', 4);

-- --------------------------------------------------------

--
-- Структура таблицы `OborType`
--

CREATE TABLE `OborType` (
  `Id` int NOT NULL,
  `Name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `OborType`
--

INSERT INTO `OborType` (`Id`, `Name`) VALUES
(1, 'Монитор'),
(2, 'Клавиатура'),
(3, 'Мышь'),
(4, 'Системный блок'),
(5, 'Стол'),
(6, 'Компьютерный стул'),
(7, 'Принтер'),
(8, 'Нет');

-- --------------------------------------------------------

--
-- Структура таблицы `Oborudovanie`
--

CREATE TABLE `Oborudovanie` (
  `Id` int NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `Photo` blob,
  `InventNumber` varchar(45) DEFAULT NULL,
  `IdClassroom` int DEFAULT NULL,
  `IdResponUser` int DEFAULT NULL,
  `IdTimeResponUser` int DEFAULT NULL,
  `PriceObor` varchar(45) DEFAULT NULL,
  `IdNapravObor` int DEFAULT NULL,
  `IdStatusObor` int DEFAULT NULL,
  `IdModelObor` int DEFAULT NULL,
  `Comments` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Oborudovanie`
--

INSERT INTO `Oborudovanie` (`Id`, `Name`, `Photo`, `InventNumber`, `IdClassroom`, `IdResponUser`, `IdTimeResponUser`, `PriceObor`, `IdNapravObor`, `IdStatusObor`, `IdModelObor`, `Comments`) VALUES
(1, 'Учебный монитор', NULL, '123456789', 3, 2, 1, '14000', 3, 3, 2, 'Без комментариев'),
(2, 'Учебная клавиатура', NULL, '987654321', 2, 1, 3, '2999', 3, 1, 3, 'Новая'),
(4, 'Персональный компьютер', NULL, '238921450', 1, 2, 1, '70000', 1, 2, 2, 'dh');

-- --------------------------------------------------------

--
-- Структура таблицы `Programs`
--

CREATE TABLE `Programs` (
  `Id` int NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `VersionPO` varchar(45) DEFAULT NULL,
  `DeveloperId` int DEFAULT NULL,
  `OborrId` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Programs`
--

INSERT INTO `Programs` (`Id`, `Name`, `VersionPO`, `DeveloperId`, `OborrId`) VALUES
(9, 'My SQL Workbench', '8.0.19', 1, 4),
(10, 'Microsoft Visual Studio', '2022', 2, 4);

-- --------------------------------------------------------

--
-- Структура таблицы `RasxodMaterials`
--

CREATE TABLE `RasxodMaterials` (
  `Id` int NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `DatePostupleniya` datetime DEFAULT NULL,
  `Photo` blob,
  `Quantity` double DEFAULT NULL,
  `UserRespon` int DEFAULT NULL,
  `ResponUserTime` int DEFAULT NULL,
  `CharacteristicsType` int DEFAULT NULL,
  `Characteristics` int DEFAULT NULL,
  `IdValue` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `RasxodMaterials`
--

INSERT INTO `RasxodMaterials` (`Id`, `Name`, `Description`, `DatePostupleniya`, `Photo`, `Quantity`, `UserRespon`, `ResponUserTime`, `CharacteristicsType`, `Characteristics`, `IdValue`) VALUES
(1, 'A4Tech OP-330', 'Фигня', '2025-03-11 00:00:00', 0x6764676467, 3, 1, 2, 1, 2, 3),
(2, 'Logitech G502 X', 'Вообще фигня', '2024-11-20 00:00:00', 0x646667646667, 4, 3, 1, 1, 2, 2);

-- --------------------------------------------------------

--
-- Структура таблицы `Status`
--

CREATE TABLE `Status` (
  `Id` int NOT NULL,
  `Name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Status`
--

INSERT INTO `Status` (`Id`, `Name`) VALUES
(1, 'В эксплуатации'),
(2, 'На складе'),
(3, 'В ремонте'),
(4, 'На выдаче'),
(5, 'На учёте'),
(7, 'Требуется обслуживание'),
(9, 'Нет статуса');

-- --------------------------------------------------------

--
-- Структура таблицы `TypeCharacteristics`
--

CREATE TABLE `TypeCharacteristics` (
  `Id` int NOT NULL,
  `Name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `TypeCharacteristics`
--

INSERT INTO `TypeCharacteristics` (`Id`, `Name`) VALUES
(1, 'Мышь'),
(2, 'Клавиатура');

-- --------------------------------------------------------

--
-- Структура таблицы `Users`
--

CREATE TABLE `Users` (
  `Id` int NOT NULL,
  `Login` varchar(45) DEFAULT NULL,
  `Password` varchar(45) DEFAULT NULL,
  `FIO` varchar(255) DEFAULT NULL,
  `Role` varchar(45) DEFAULT NULL,
  `Email` varchar(45) DEFAULT NULL,
  `Number` varchar(45) DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Users`
--

INSERT INTO `Users` (`Id`, `Login`, `Password`, `FIO`, `Role`, `Email`, `Number`, `Address`) VALUES
(1, 'dashka', 'asd123', 'Ротанова Дарья Валерьевна', 'Администратор', 'fkdgk@gmail.com', '79001112233', 'г. Пермь, ул. Пушкина, 7'),
(2, 'anastasiyapl', 'qwerty123', 'Плешкова Анастасия Александровна', 'Сотрудник', 'isoaj@mail.ru', '79082221100', 'г. Пермь, ул. Монастырская, 11'),
(3, 'milosh', 'asdfg12', 'Мутагарова Миляуша Айратовна', 'Преподаватель', 'oasij@bk.ru', '79223334455', 'г. Пермь, ул. Луначарского, 24');

-- --------------------------------------------------------

--
-- Структура таблицы `ValueCharacteristics`
--

CREATE TABLE `ValueCharacteristics` (
  `Id` int NOT NULL,
  `IdRasxod` int DEFAULT NULL,
  `IdCharacter` int DEFAULT NULL,
  `Znachenie` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `ValueCharacteristics`
--

INSERT INTO `ValueCharacteristics` (`Id`, `IdRasxod`, `IdCharacter`, `Znachenie`) VALUES
(1, 2, 3, '1590'),
(2, 1, 2, 'Лазерная'),
(3, 1, 3, 'Пластик'),
(4, 2, 1, 'Серебристая'),
(5, 2, 2, 'Оптическая'),
(6, 2, 3, 'Металлическая');

-- --------------------------------------------------------

--
-- Структура таблицы `ViewModel`
--

CREATE TABLE `ViewModel` (
  `Id` int NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `OborType` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `ViewModel`
--

INSERT INTO `ViewModel` (`Id`, `Name`, `OborType`) VALUES
(1, 'Brother HL-1210W', 7),
(2, 'MSI PRO MP273A', 1),
(3, 'Logitech K280E', 2),
(4, 'Logitech G102 LIGHTSYNC', 3),
(6, 'Нет модели', 8);

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Auditories`
--
ALTER TABLE `Auditories`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `ResponUser_idx` (`ResponUser`),
  ADD KEY `TimeResponUser_idx` (`TimeResponUser`);

--
-- Индексы таблицы `Characteristics`
--
ALTER TABLE `Characteristics`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `TypeCharacter_idx` (`TypeCharacter`);

--
-- Индексы таблицы `Developers`
--
ALTER TABLE `Developers`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `Errors`
--
ALTER TABLE `Errors`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `HistoryInventory`
--
ALTER TABLE `HistoryInventory`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `OborId_idx` (`OborId`),
  ADD KEY `IdUser_idx` (`IdUser`);

--
-- Индексы таблицы `HistoryObor`
--
ALTER TABLE `HistoryObor`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdUserr_idx` (`IdUserr`),
  ADD KEY `IdObor_idx` (`IdObor`);

--
-- Индексы таблицы `Inventory`
--
ALTER TABLE `Inventory`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `UserId_idx` (`UserId`),
  ADD KEY `IdOborrud_idx` (`IdOborrud`);

--
-- Индексы таблицы `Napravlenie`
--
ALTER TABLE `Napravlenie`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `NetworkSettings`
--
ALTER TABLE `NetworkSettings`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `OborudovanieId_idx` (`OborudovanieId`);

--
-- Индексы таблицы `OborType`
--
ALTER TABLE `OborType`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `Oborudovanie`
--
ALTER TABLE `Oborudovanie`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdClassroom_idx` (`IdClassroom`),
  ADD KEY `IdResponUser_idx` (`IdResponUser`),
  ADD KEY `IdTimeResponUser_idx` (`IdTimeResponUser`),
  ADD KEY `IdNapravObor_idx` (`IdNapravObor`),
  ADD KEY `IdStatusObor_idx` (`IdStatusObor`),
  ADD KEY `IdModelObor_idx` (`IdModelObor`);

--
-- Индексы таблицы `Programs`
--
ALTER TABLE `Programs`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `DeveloperId_idx` (`DeveloperId`),
  ADD KEY `OborrId_idx` (`OborrId`);

--
-- Индексы таблицы `RasxodMaterials`
--
ALTER TABLE `RasxodMaterials`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `UserRespon_idx` (`UserRespon`),
  ADD KEY `ResponUserTime_idx` (`ResponUserTime`),
  ADD KEY `CharacteristicsType_idx` (`CharacteristicsType`),
  ADD KEY `IdValue_idx` (`IdValue`),
  ADD KEY `Characteristics_idx` (`Characteristics`);

--
-- Индексы таблицы `Status`
--
ALTER TABLE `Status`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `TypeCharacteristics`
--
ALTER TABLE `TypeCharacteristics`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `ValueCharacteristics`
--
ALTER TABLE `ValueCharacteristics`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdRasxod_idx` (`IdRasxod`),
  ADD KEY `IdCharacter_idx` (`IdCharacter`);

--
-- Индексы таблицы `ViewModel`
--
ALTER TABLE `ViewModel`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `OborType_idx` (`OborType`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Auditories`
--
ALTER TABLE `Auditories`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT для таблицы `Characteristics`
--
ALTER TABLE `Characteristics`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT для таблицы `Developers`
--
ALTER TABLE `Developers`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT для таблицы `Errors`
--
ALTER TABLE `Errors`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT для таблицы `HistoryInventory`
--
ALTER TABLE `HistoryInventory`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `HistoryObor`
--
ALTER TABLE `HistoryObor`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `Inventory`
--
ALTER TABLE `Inventory`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT для таблицы `Napravlenie`
--
ALTER TABLE `Napravlenie`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT для таблицы `NetworkSettings`
--
ALTER TABLE `NetworkSettings`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `OborType`
--
ALTER TABLE `OborType`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT для таблицы `Oborudovanie`
--
ALTER TABLE `Oborudovanie`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT для таблицы `Programs`
--
ALTER TABLE `Programs`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT для таблицы `RasxodMaterials`
--
ALTER TABLE `RasxodMaterials`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT для таблицы `Status`
--
ALTER TABLE `Status`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT для таблицы `TypeCharacteristics`
--
ALTER TABLE `TypeCharacteristics`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `Users`
--
ALTER TABLE `Users`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT для таблицы `ValueCharacteristics`
--
ALTER TABLE `ValueCharacteristics`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT для таблицы `ViewModel`
--
ALTER TABLE `ViewModel`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `Auditories`
--
ALTER TABLE `Auditories`
  ADD CONSTRAINT `ResponUser` FOREIGN KEY (`ResponUser`) REFERENCES `Users` (`Id`),
  ADD CONSTRAINT `TimeResponUser` FOREIGN KEY (`TimeResponUser`) REFERENCES `Users` (`Id`);

--
-- Ограничения внешнего ключа таблицы `Characteristics`
--
ALTER TABLE `Characteristics`
  ADD CONSTRAINT `TypeCharacter` FOREIGN KEY (`TypeCharacter`) REFERENCES `TypeCharacteristics` (`Id`);

--
-- Ограничения внешнего ключа таблицы `HistoryInventory`
--
ALTER TABLE `HistoryInventory`
  ADD CONSTRAINT `IdUser` FOREIGN KEY (`IdUser`) REFERENCES `Users` (`Id`),
  ADD CONSTRAINT `OborId` FOREIGN KEY (`OborId`) REFERENCES `Oborudovanie` (`Id`);

--
-- Ограничения внешнего ключа таблицы `HistoryObor`
--
ALTER TABLE `HistoryObor`
  ADD CONSTRAINT `IdObor` FOREIGN KEY (`IdObor`) REFERENCES `Oborudovanie` (`Id`),
  ADD CONSTRAINT `IdUserr` FOREIGN KEY (`IdUserr`) REFERENCES `Users` (`Id`);

--
-- Ограничения внешнего ключа таблицы `Inventory`
--
ALTER TABLE `Inventory`
  ADD CONSTRAINT `IdOborrud` FOREIGN KEY (`IdOborrud`) REFERENCES `Oborudovanie` (`Id`),
  ADD CONSTRAINT `UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`);

--
-- Ограничения внешнего ключа таблицы `NetworkSettings`
--
ALTER TABLE `NetworkSettings`
  ADD CONSTRAINT `OborudovanieId` FOREIGN KEY (`OborudovanieId`) REFERENCES `Oborudovanie` (`Id`);

--
-- Ограничения внешнего ключа таблицы `Oborudovanie`
--
ALTER TABLE `Oborudovanie`
  ADD CONSTRAINT `IdClassroom` FOREIGN KEY (`IdClassroom`) REFERENCES `Auditories` (`Id`),
  ADD CONSTRAINT `IdModelObor` FOREIGN KEY (`IdModelObor`) REFERENCES `ViewModel` (`Id`),
  ADD CONSTRAINT `IdNapravObor` FOREIGN KEY (`IdNapravObor`) REFERENCES `Napravlenie` (`Id`),
  ADD CONSTRAINT `IdResponUser` FOREIGN KEY (`IdResponUser`) REFERENCES `Users` (`Id`),
  ADD CONSTRAINT `IdStatusObor` FOREIGN KEY (`IdStatusObor`) REFERENCES `Status` (`Id`),
  ADD CONSTRAINT `IdTimeResponUser` FOREIGN KEY (`IdTimeResponUser`) REFERENCES `Users` (`Id`);

--
-- Ограничения внешнего ключа таблицы `Programs`
--
ALTER TABLE `Programs`
  ADD CONSTRAINT `DeveloperId` FOREIGN KEY (`DeveloperId`) REFERENCES `Developers` (`Id`),
  ADD CONSTRAINT `OborrId` FOREIGN KEY (`OborrId`) REFERENCES `Oborudovanie` (`Id`);

--
-- Ограничения внешнего ключа таблицы `RasxodMaterials`
--
ALTER TABLE `RasxodMaterials`
  ADD CONSTRAINT `Characteristics` FOREIGN KEY (`Characteristics`) REFERENCES `Characteristics` (`Id`),
  ADD CONSTRAINT `CharacteristicsType` FOREIGN KEY (`CharacteristicsType`) REFERENCES `TypeCharacteristics` (`Id`),
  ADD CONSTRAINT `IdValue` FOREIGN KEY (`IdValue`) REFERENCES `ValueCharacteristics` (`Id`),
  ADD CONSTRAINT `ResponUserTime` FOREIGN KEY (`ResponUserTime`) REFERENCES `Users` (`Id`),
  ADD CONSTRAINT `UserRespon` FOREIGN KEY (`UserRespon`) REFERENCES `Users` (`Id`);

--
-- Ограничения внешнего ключа таблицы `ValueCharacteristics`
--
ALTER TABLE `ValueCharacteristics`
  ADD CONSTRAINT `IdCharacter` FOREIGN KEY (`IdCharacter`) REFERENCES `Characteristics` (`Id`),
  ADD CONSTRAINT `IdRasxod` FOREIGN KEY (`IdRasxod`) REFERENCES `RasxodMaterials` (`Id`);

--
-- Ограничения внешнего ключа таблицы `ViewModel`
--
ALTER TABLE `ViewModel`
  ADD CONSTRAINT `OborType` FOREIGN KEY (`OborType`) REFERENCES `OborType` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
