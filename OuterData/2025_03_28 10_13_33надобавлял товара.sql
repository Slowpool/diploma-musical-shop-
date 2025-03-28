-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: musical_shop
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `musical_shop`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `musical_shop` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `musical_shop`;

--
-- Table structure for table `accessories`
--

DROP TABLE IF EXISTS `accessories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accessories` (
  `goods_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `color` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `size` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `soft_deleted` tinyint(1) NOT NULL,
  `price` int NOT NULL,
  `status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `specific_type_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `receipt_date` datetime(6) DEFAULT NULL,
  `delivery_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`goods_id`),
  KEY `ix_accessories_specific_type_id` (`specific_type_id`),
  KEY `ix_accessories_delivery_id` (`delivery_id`),
  CONSTRAINT `fk_accessories_accessory_specific_types_specific_type_id` FOREIGN KEY (`specific_type_id`) REFERENCES `accessory_specific_types` (`specific_type_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_accessories_goods_delivery_delivery_id` FOREIGN KEY (`delivery_id`) REFERENCES `goods_delivery` (`goods_delivery_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accessories`
--

LOCK TABLES `accessories` WRITE;
/*!40000 ALTER TABLE `accessories` DISABLE KEYS */;
INSERT INTO `accessories` VALUES ('05812ce5-61c0-4eaf-1937-aeeb653b2191','Прозрачный','регулировка высоты от 10 до 150 см, 50см радиус седла',0,599,'Sold','Круглая табуретка','Табуретка','08dd6dd1-c922-4946-8abd-0665d77c7aa1','2023-10-12 07:20:35.000000',NULL),('08dd6dd2-be8f-4779-8a09-65d34394ba17','Металлический','90см длина, 0.11см толщина, форма цилиндрическая. В карман не поместится, в сумку тоже.',0,199,'InStock','Непревзойденно оригинальный брелок.','Брелок с гитарной струной','08dd6dd1-c923-4f47-8276-65819bcce26b','2025-03-28 08:30:06.402401','993c6450-a034-44c1-a43c-668109008d54'),('08dd6dd2-be8f-4e76-80de-d1633d6c200e','Металлический','90см длина, 0.11см толщина, форма цилиндрическая. В карман не поместится, в сумку тоже.',0,199,'InStock','Непревзойденно оригинальный брелок.','Брелок с гитарной струной','08dd6dd1-c923-4f47-8276-65819bcce26b','2025-03-28 08:30:06.402401','993c6450-a034-44c1-a43c-668109008d54'),('08dd6dd2-be8f-4ea2-85ab-723f2dc87a7b','Металлический','90см длина, 0.11см толщина, форма цилиндрическая. В карман не поместится, в сумку тоже.',0,199,'InStock','Непревзойденно оригинальный брелок.','Брелок с гитарной струной','08dd6dd1-c923-4f47-8276-65819bcce26b','2025-03-28 08:30:06.402401','993c6450-a034-44c1-a43c-668109008d54'),('08dd6ddc-ed7c-44bd-88bb-bb1227b99cc4','Задумчиво-белый','От стопы до колена среднестатистического человека',0,990,'InStock','Создана мастерами Китая чтобы сидеть','Табуретка \"Посидим, а там видно будет\"','08dd6dd1-c922-4946-8abd-0665d77c7aa1','2025-03-28 09:43:00.103679','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddc-ed7c-4b30-8dd2-06cba305cc6d','Задумчиво-белый','От стопы до колена среднестатистического человека',0,990,'InStock','Создана мастерами Китая чтобы сидеть','Табуретка \"Посидим, а там видно будет\"','08dd6dd1-c922-4946-8abd-0665d77c7aa1','2025-03-28 09:43:00.103679','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddc-ed7c-4b5e-898a-a8b273b60b7b','Задумчиво-белый','От стопы до колена среднестатистического человека',0,990,'InStock','Создана мастерами Китая чтобы сидеть','Табуретка \"Посидим, а там видно будет\"','08dd6dd1-c922-4946-8abd-0665d77c7aa1','2025-03-28 09:43:00.103679','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6dde-c114-4581-8556-76cd13b35aa9','Черный','Размер зависит от высоты на уровнем моря: +1 метр к высоте = +1 см к размеру. Размер на уровне океана: 1см.',0,7600,'AwaitingDelivery','При полете издает звук на той высоте на которой он летит. Высота измеряется относительно уровня океана, 1 метр = 1 полутон. корневая нота (на уровне океана) - C3','Бумеранг в форме ноты','08dd6dde-c113-4e67-8b32-435bfac3092b',NULL,'82a5e559-7a98-43a5-9af3-04ab5fe861cf'),('08dd6dde-c114-4611-869f-c637ae4a0b96','Черный','Размер зависит от высоты на уровнем моря: +1 метр к высоте = +1 см к размеру. Размер на уровне океана: 1см.',0,7600,'AwaitingDelivery','При полете издает звук на той высоте на которой он летит. Высота измеряется относительно уровня океана, 1 метр = 1 полутон. корневая нота (на уровне океана) - C3','Бумеранг в форме ноты','08dd6dde-c113-4e67-8b32-435bfac3092b',NULL,'82a5e559-7a98-43a5-9af3-04ab5fe861cf'),('08dd6dde-c114-4634-825b-a3b831cd1ef0','Черный','Размер зависит от высоты на уровнем моря: +1 метр к высоте = +1 см к размеру. Размер на уровне океана: 1см.',0,7600,'AwaitingDelivery','При полете издает звук на той высоте на которой он летит. Высота измеряется относительно уровня океана, 1 метр = 1 полутон. корневая нота (на уровне океана) - C3','Бумеранг в форме ноты','08dd6dde-c113-4e67-8b32-435bfac3092b',NULL,'82a5e559-7a98-43a5-9af3-04ab5fe861cf'),('08dd6dde-c114-464a-8034-9ae337fce618','Черный','Размер зависит от высоты на уровнем моря: +1 метр к высоте = +1 см к размеру. Размер на уровне океана: 1см.',0,7600,'AwaitingDelivery','При полете издает звук на той высоте на которой он летит. Высота измеряется относительно уровня океана, 1 метр = 1 полутон. корневая нота (на уровне океана) - C3','Бумеранг в форме ноты','08dd6dde-c113-4e67-8b32-435bfac3092b',NULL,'82a5e559-7a98-43a5-9af3-04ab5fe861cf'),('08dd6ddf-4396-454b-8c02-c920076bc016','Прозрачный','Помещается в зрачке',0,790,'AwaitingDelivery','Позволяет настроить звук на одну ноту. После выбора ноты ее нельзя изменить - изменения прикладываются ко внутреннему устройству физически и доступны для изменения только при вмешательстве мастера.','Тюнер однонотный','08dd6ddf-4394-4400-83b1-8571fbc0c45b',NULL,'82a5e559-7a98-43a5-9af3-04ab5fe861cf'),('08dd6ddf-e0ed-4853-8375-19bff7b9d1ac','Черно-белые','М',0,2900,'AwaitingDelivery','Данные футболки отличные.','Футболки парные \"ДоМажорыч и ЛяМинорыч\"','08dd6ddf-e0ec-4ac2-8395-83d2c1a29aab',NULL,'82a5e559-7a98-43a5-9af3-04ab5fe861cf'),('08dd6de0-b4e5-4351-8f8f-713346a05419','Адовый','Цилиндрический, 10см диаметр',0,4900,'AwaitingDelivery','Данный будильник издает мелодию, состоящую только из интервала тритон. Человек, шарящий в интервалах проснется за час до этого будильника чтобы не слышать его проигрывание.','Будильник \"Антимузыкантыч\"','08dd6de0-b4e4-483b-8fc2-ee87dfb7cb3b',NULL,'82a5e559-7a98-43a5-9af3-04ab5fe861cf'),('08dd6de0-b4e5-43b7-84f7-bfc88a487c59','Адовый','Цилиндрический, 10см диаметр',0,4900,'AwaitingDelivery','Данный будильник издает мелодию, состоящую только из интервала тритон. Человек, шарящий в интервалах проснется за час до этого будильника чтобы не слышать его проигрывание.','Будильник \"Антимузыкантыч\"','08dd6de0-b4e4-483b-8fc2-ee87dfb7cb3b',NULL,'82a5e559-7a98-43a5-9af3-04ab5fe861cf'),('6d4e31a0-9809-44c8-810f-7e0c4f435e03','Черно-рыжий','Высота пюпитра: 30-200см. Каподастр 13см x 1см x 12 см',0,699,'Reserved','Набор 3 в 1: пюпитр и каподастр','Стартующий гитарист','08dd6dd1-c923-4f47-8276-65819bcce26b','2023-10-12 07:20:35.000000',NULL),('bf73bc1d-5d82-460b-9cf4-cd08e117face','Черно-желтый','20см x 0.5см x 3см',0,99,'Sold','Брелок с граммофоном отлично смотрится на архивных вещах','Брелок с граммофоном','08dd6dd1-c923-4f47-8276-65819bcce26b','2023-10-12 07:20:35.000000',NULL);
/*!40000 ALTER TABLE `accessories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `accessory_sale`
--

DROP TABLE IF EXISTS `accessory_sale`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accessory_sale` (
  `accessory_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `sale_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  PRIMARY KEY (`sale_id`,`accessory_id`),
  KEY `ix_accessory_sale_accessory_id` (`accessory_id`),
  CONSTRAINT `FK_sale_accessory_id` FOREIGN KEY (`accessory_id`) REFERENCES `accessories` (`goods_id`) ON DELETE CASCADE,
  CONSTRAINT `FK_sale_accessory_sale_id` FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accessory_sale`
--

LOCK TABLES `accessory_sale` WRITE;
/*!40000 ALTER TABLE `accessory_sale` DISABLE KEYS */;
INSERT INTO `accessory_sale` VALUES ('05812ce5-61c0-4eaf-1937-aeeb653b2191','01937ce5-61c0-4eaf-8580-aeeb653b2191'),('6d4e31a0-9809-44c8-810f-7e0c4f435e03','bf1cf858-f491-4234-9331-0b4abef9f0e8'),('bf73bc1d-5d82-460b-9cf4-cd08e117face','286b3185-c983-4339-86c9-b12fc8fac5e2');
/*!40000 ALTER TABLE `accessory_sale` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `accessory_specific_types`
--

DROP TABLE IF EXISTS `accessory_specific_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accessory_specific_types` (
  `specific_type_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`specific_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accessory_specific_types`
--

LOCK TABLES `accessory_specific_types` WRITE;
/*!40000 ALTER TABLE `accessory_specific_types` DISABLE KEYS */;
INSERT INTO `accessory_specific_types` VALUES ('08dd6dd1-c922-4946-8abd-0665d77c7aa1','Табуретка регулируемая'),('08dd6dd1-c923-4f47-8276-65819bcce26b','Брелок'),('08dd6dde-c113-4e67-8b32-435bfac3092b','Бумеранг'),('08dd6ddf-4394-4400-83b1-8571fbc0c45b','Тюнер'),('08dd6ddf-e0ec-4ac2-8395-83d2c1a29aab','Набор из двух одежд'),('08dd6de0-b4e4-483b-8fc2-ee87dfb7cb3b','Будильник');
/*!40000 ALTER TABLE `accessory_specific_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `audio_equipment_unit_sale`
--

DROP TABLE IF EXISTS `audio_equipment_unit_sale`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `audio_equipment_unit_sale` (
  `audio_equipment_unit_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `sale_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  PRIMARY KEY (`sale_id`,`audio_equipment_unit_id`),
  KEY `ix_audio_equipment_unit_sale_audio_equipment_unit_id` (`audio_equipment_unit_id`),
  CONSTRAINT `FK_sale_aeu_id` FOREIGN KEY (`audio_equipment_unit_id`) REFERENCES `audio_equipment_units` (`goods_id`) ON DELETE CASCADE,
  CONSTRAINT `FK_sale_aeu_sale_id` FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `audio_equipment_unit_sale`
--

LOCK TABLES `audio_equipment_unit_sale` WRITE;
/*!40000 ALTER TABLE `audio_equipment_unit_sale` DISABLE KEYS */;
/*!40000 ALTER TABLE `audio_equipment_unit_sale` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `audio_equipment_unit_specific_types`
--

DROP TABLE IF EXISTS `audio_equipment_unit_specific_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `audio_equipment_unit_specific_types` (
  `specific_type_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`specific_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `audio_equipment_unit_specific_types`
--

LOCK TABLES `audio_equipment_unit_specific_types` WRITE;
/*!40000 ALTER TABLE `audio_equipment_unit_specific_types` DISABLE KEYS */;
INSERT INTO `audio_equipment_unit_specific_types` VALUES ('08dd6ddd-254c-469f-8439-688c41ac8046','Наушники'),('08dd6ddd-71fe-461f-8dc7-0b46594b52dc','Микрофон потолочный');
/*!40000 ALTER TABLE `audio_equipment_unit_specific_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `audio_equipment_units`
--

DROP TABLE IF EXISTS `audio_equipment_units`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `audio_equipment_units` (
  `goods_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `soft_deleted` tinyint(1) NOT NULL,
  `price` int NOT NULL,
  `status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `specific_type_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `receipt_date` datetime(6) DEFAULT NULL,
  `delivery_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`goods_id`),
  KEY `ix_audio_equipment_units_specific_type_id` (`specific_type_id`),
  KEY `ix_audio_equipment_units_delivery_id` (`delivery_id`),
  CONSTRAINT `fk_audio_equipment_units_audio_equipment_unit_specific_types_sp` FOREIGN KEY (`specific_type_id`) REFERENCES `audio_equipment_unit_specific_types` (`specific_type_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_audio_equipment_units_goods_delivery_delivery_id` FOREIGN KEY (`delivery_id`) REFERENCES `goods_delivery` (`goods_delivery_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `audio_equipment_units`
--

LOCK TABLES `audio_equipment_units` WRITE;
/*!40000 ALTER TABLE `audio_equipment_units` DISABLE KEYS */;
INSERT INTO `audio_equipment_units` VALUES ('08dd6ddd-254e-4895-8009-ec8a9c999e95',0,590,'InStock','Созданы складским менеджером из бракованных колонок','Наушники \"Колонки на скотче\"','08dd6ddd-254c-469f-8439-688c41ac8046','2025-03-28 09:44:33.755819','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddd-71ff-4c1c-8286-c137bb6074d3',0,3990,'InStock','Создан на севере антарктиды','Микрофон потолочный \"Аурус созвездие\"','08dd6ddd-71fe-461f-8dc7-0b46594b52dc','2025-03-28 09:46:42.426456','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddd-71ff-4cbb-87b8-3bd47d3fb269',0,3990,'InStock','Создан на севере антарктиды','Микрофон потолочный \"Аурус созвездие\"','08dd6ddd-71fe-461f-8dc7-0b46594b52dc','2025-03-28 09:46:42.426456','795843e7-b1b4-4d21-917b-ce15f7fb926a');
/*!40000 ALTER TABLE `audio_equipment_units` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `goods_delivery`
--

DROP TABLE IF EXISTS `goods_delivery`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `goods_delivery` (
  `goods_delivery_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `expected_delivery_date` datetime(6) DEFAULT NULL,
  `actual_delivery_date` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`goods_delivery_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `goods_delivery`
--

LOCK TABLES `goods_delivery` WRITE;
/*!40000 ALTER TABLE `goods_delivery` DISABLE KEYS */;
INSERT INTO `goods_delivery` VALUES ('795843e7-b1b4-4d21-917b-ce15f7fb926a',NULL,'2025-03-28 09:43:00.103560'),('82038062-2775-4098-8726-b585b37ae923',NULL,'2025-03-28 09:37:03.230540'),('82a5e559-7a98-43a5-9af3-04ab5fe861cf',NULL,'2025-03-28 09:56:04.598040'),('993c6450-a034-44c1-a43c-668109008d54',NULL,'2025-03-28 08:30:06.390891');
/*!40000 ALTER TABLE `goods_delivery` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `musical_instrument_sale`
--

DROP TABLE IF EXISTS `musical_instrument_sale`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `musical_instrument_sale` (
  `musical_instrument_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `sale_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  PRIMARY KEY (`sale_id`,`musical_instrument_id`),
  KEY `ix_musical_instrument_sale_musical_instrument_id` (`musical_instrument_id`),
  CONSTRAINT `FK_sale_musical_instrument_id` FOREIGN KEY (`musical_instrument_id`) REFERENCES `musical_instruments` (`goods_id`) ON DELETE CASCADE,
  CONSTRAINT `FK_sale_musical_instrument_sale_id` FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `musical_instrument_sale`
--

LOCK TABLES `musical_instrument_sale` WRITE;
/*!40000 ALTER TABLE `musical_instrument_sale` DISABLE KEYS */;
INSERT INTO `musical_instrument_sale` VALUES ('d7b2ff21-cc80-41fa-bef5-3ca93c5ec4fa','286b3185-c983-4339-86c9-b12fc8fac5e2'),('ee1c5679-2018-4192-8e02-6efed0ef8c5a','01937ce5-61c0-4eaf-8580-aeeb653b2191');
/*!40000 ALTER TABLE `musical_instrument_sale` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `musical_instrument_specific_types`
--

DROP TABLE IF EXISTS `musical_instrument_specific_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `musical_instrument_specific_types` (
  `specific_type_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`specific_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `musical_instrument_specific_types`
--

LOCK TABLES `musical_instrument_specific_types` WRITE;
/*!40000 ALTER TABLE `musical_instrument_specific_types` DISABLE KEYS */;
INSERT INTO `musical_instrument_specific_types` VALUES ('08dd6dd1-c90d-4df2-838c-f3b98e14e432','Акустическая гитара'),('08dd6dd1-c910-407a-82c5-451f1d553dc6','Барабанная установка'),('08dd6dd1-c910-41d9-8c20-d5aa1c09402e','Флейта'),('08dd6dd1-c916-45c2-8426-f8aa789a9d49','Синтезатор'),('08dd6ddc-18b9-413d-81ed-d91073d84867','Никельхарпа'),('08dd6ddc-8530-4e9b-874d-6927e896f425','Кларнет');
/*!40000 ALTER TABLE `musical_instrument_specific_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `musical_instruments`
--

DROP TABLE IF EXISTS `musical_instruments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `musical_instruments` (
  `goods_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `release_year` int NOT NULL,
  `manufacturer` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `manufacturer_type` int NOT NULL,
  `soft_deleted` tinyint(1) NOT NULL,
  `price` int NOT NULL,
  `status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `specific_type_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `receipt_date` datetime(6) DEFAULT NULL,
  `delivery_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`goods_id`),
  KEY `ix_musical_instruments_specific_type_id` (`specific_type_id`),
  KEY `ix_musical_instruments_delivery_id` (`delivery_id`),
  CONSTRAINT `fk_musical_instruments_goods_delivery_delivery_id` FOREIGN KEY (`delivery_id`) REFERENCES `goods_delivery` (`goods_delivery_id`),
  CONSTRAINT `fk_musical_instruments_musical_instrument_specific_types_specif` FOREIGN KEY (`specific_type_id`) REFERENCES `musical_instrument_specific_types` (`specific_type_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `musical_instruments`
--

LOCK TABLES `musical_instruments` WRITE;
/*!40000 ALTER TABLE `musical_instruments` DISABLE KEYS */;
INSERT INTO `musical_instruments` VALUES ('05812ce5-61c0-4eaf-8580-aeeb653b2191',2023,'John Spelberg',1,0,9599,'InStock','Акустическая гитара, с вырезом, санберст, Foix','FFG-3860C-SB','08dd6dd1-c90d-4df2-838c-f3b98e14e432','2023-10-12 07:20:35.000000',NULL),('08dd6ddc-18c9-4388-8506-91a0f3190c37',2023,'Шереметьевский завод никельхарп и брелков',0,0,19490,'InStock','Автоматическая скрипка','Никельхарпа N3500','08dd6ddc-18b9-413d-81ed-d91073d84867','2025-03-28 09:37:03.242741','82038062-2775-4098-8726-b585b37ae923'),('08dd6ddc-18cb-44f6-8c30-02d2c3228776',2023,'Шереметьевский завод никельхарп и брелков',0,0,19490,'InStock','Автоматическая скрипка','Никельхарпа N3500','08dd6ddc-18b9-413d-81ed-d91073d84867','2025-03-28 09:37:03.242741','82038062-2775-4098-8726-b585b37ae923'),('08dd6ddc-8537-4f36-80c0-ee9ebbc17b14',2021,'Сомов И. З.',1,0,182790,'AwaitingDelivery','Позволяет рыбачить и играть песни одновременно','Кларнет с функцией удочки','08dd6ddc-8530-4e9b-874d-6927e896f425',NULL,'82038062-2775-4098-8726-b585b37ae923'),('0e05ca0d-7e34-4b65-a5fc-3e7b69194390',2021,'Завод гитар для котов',0,0,2399,'AwaitingDelivery','Акустическая гитара, без розетки, санберст отсутствует, полые порожки, струны из дерева, ж/б гриф, встроенная когтеточка, удобная лежанка и автокормушка с функцией будильника.','APPOLON-19-SUNBURSTLESS','08dd6dd1-c90d-4df2-838c-f3b98e14e432',NULL,NULL),('148b3083-f123-44df-8fce-af4c7014ac31',2022,'Завод гитар имени Инемиратигдова З.',0,0,3499,'InStock','Акустическая гитара, без порожков, Nice','L529-9X8823L-CCAS3-AR-2IC-3H-1SAEID50-BEZ-POROJKOF-OTVALILIS\':(','08dd6dd1-c90d-4df2-838c-f3b98e14e432','2022-01-09 22:20:35.000000',NULL),('2b14e3ec-6af4-4094-ba4b-255933603cc9',2019,'Барабанный лидер',0,0,14900,'AwaitingDelivery','*Барабанная дробь*... Барабанная установка \"Барабанная мечта\" - барабанный рай барабанного любителя.','Стукач','08dd6dd1-c910-407a-82c5-451f1d553dc6','2024-01-09 22:20:35.000000',NULL),('32eb21b6-6d88-42aa-9326-114297689a59',2020,'Завод собачьих гитар',0,0,2399,'AwaitingDelivery','Акустическая гитара, без выреза, артишок, Belucci','LDPWD','08dd6dd1-c90d-4df2-838c-f3b98e14e432',NULL,NULL),('6f5c6af2-6fb7-4cf2-8730-5e365d2c1032',2024,'John Maloe',1,0,88990,'InStock','Акустическая гитара, без выреза, но с вырезом, проивзедено в США','MyFirstGuitar','08dd6dd1-c90d-4df2-838c-f3b98e14e432','2022-01-09 22:20:35.000000',NULL),('9384b7c1-6727-4dd0-88cc-7e1a1d9062cb',2023,'John Spelberg',1,0,7499,'Reserved',' Акустическая гитара, без выреза, без санберста, Hoix','Kolenval-SB-SUNBRESTLESS','08dd6dd1-c90d-4df2-838c-f3b98e14e432','2024-10-12 07:20:35.000000',NULL),('9d7e4b3b-cbaa-4327-af1c-1ea3e232d68a',1975,'Steve Pianoe',1,0,11590,'InStock','Компактное пианино, 3 режима, подсветка, присутствует нейросеть, позволяющая схватывать колебания головного мозга с целью воспроизведения желаемой мелодии. Сделано в СССР','Sntzr-1937','08dd6dd1-c916-45c2-8426-f8aa789a9d49','2023-01-09 22:20:35.000000',NULL),('a07319f6-4944-4f06-bb1c-b77c27e73b1d',2019,'Барабань-ка',0,0,1900,'InStock','Многослойные барабаны позволят слышать себя непревзойденно.','Knocker-knocker','08dd6dd1-c910-407a-82c5-451f1d553dc6','2024-01-09 22:20:35.000000',NULL),('d7b2ff21-cc80-41fa-bef5-3ca93c5ec4fa',2022,'Синтезаторы? Производим.',0,0,2390,'Sold','Данный синтезатор изготовлен из нержавеющего пластика, слоновьего зуба и экранированного хлеба. Корпус выполнен в командной строке.','Bearded?Bear?Beer?Breed?Bread?','08dd6dd1-c916-45c2-8426-f8aa789a9d49','2023-01-09 22:20:35.000000',NULL),('ee1c5679-2018-4192-8e02-6efed0ef8c5a',2023,'Завод барабанных флейт имени Дыхалова',0,0,299,'Sold','Народный духовой инструмент','[oOo]','08dd6dd1-c910-41d9-8c20-d5aa1c09402e','2024-01-09 22:20:35.000000',NULL);
/*!40000 ALTER TABLE `musical_instruments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reservations`
--

DROP TABLE IF EXISTS `reservations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reservations` (
  `reservation_extra_info_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `secret_word` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `soft_deleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`reservation_extra_info_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reservations`
--

LOCK TABLES `reservations` WRITE;
/*!40000 ALTER TABLE `reservations` DISABLE KEYS */;
/*!40000 ALTER TABLE `reservations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales`
--

DROP TABLE IF EXISTS `sales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sales` (
  `sale_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `sale_date` datetime(6) DEFAULT NULL,
  `reservation_date` datetime(6) DEFAULT NULL,
  `returning_date` datetime(6) DEFAULT NULL,
  `status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `paid_by` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `is_paid` tinyint(1) NOT NULL,
  `soft_deleted` tinyint(1) NOT NULL DEFAULT '0',
  `reservation_extra_info_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`sale_id`),
  UNIQUE KEY `ix_sales_reservation_extra_info_id` (`reservation_extra_info_id`),
  CONSTRAINT `fk_sales_reservations_reservation_extra_info_id` FOREIGN KEY (`reservation_extra_info_id`) REFERENCES `reservations` (`reservation_extra_info_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales`
--

LOCK TABLES `sales` WRITE;
/*!40000 ALTER TABLE `sales` DISABLE KEYS */;
INSERT INTO `sales` VALUES ('01937ce5-61c0-4eaf-8580-aeeb653b2191','2023-10-01 07:49:07.000000',NULL,NULL,'Sold','BankCard',1,0,NULL),('286b3185-c983-4339-86c9-b12fc8fac5e2','2023-03-01 10:20:35.000000',NULL,'2023-04-01 10:20:35.000000','Returned','Cash',1,0,NULL),('bf1cf858-f491-4234-9331-0b4abef9f0e8',NULL,'2023-07-09 07:11:35.000000',NULL,'Reserved',NULL,0,0,NULL);
/*!40000 ALTER TABLE `sales` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `sales_view`
--

DROP TABLE IF EXISTS `sales_view`;
/*!50001 DROP VIEW IF EXISTS `sales_view`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `sales_view` AS SELECT 
 1 AS `sale_id`,
 1 AS `sale_date`,
 1 AS `reservation_date`,
 1 AS `returning_date`,
 1 AS `status`,
 1 AS `total`,
 1 AS `paid_by`,
 1 AS `goods_units_count`,
 1 AS `is_paid`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `sheet_music_edition_sale`
--

DROP TABLE IF EXISTS `sheet_music_edition_sale`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sheet_music_edition_sale` (
  `sheet_music_edition_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `sale_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  PRIMARY KEY (`sale_id`,`sheet_music_edition_id`),
  KEY `ix_sheet_music_edition_sale_sheet_music_edition_id` (`sheet_music_edition_id`),
  CONSTRAINT `FK_sale_sme_id` FOREIGN KEY (`sheet_music_edition_id`) REFERENCES `sheet_music_editions` (`goods_id`) ON DELETE CASCADE,
  CONSTRAINT `FK_sale_sme_sale_id` FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sheet_music_edition_sale`
--

LOCK TABLES `sheet_music_edition_sale` WRITE;
/*!40000 ALTER TABLE `sheet_music_edition_sale` DISABLE KEYS */;
/*!40000 ALTER TABLE `sheet_music_edition_sale` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sheet_music_edition_specific_types`
--

DROP TABLE IF EXISTS `sheet_music_edition_specific_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sheet_music_edition_specific_types` (
  `specific_type_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`specific_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sheet_music_edition_specific_types`
--

LOCK TABLES `sheet_music_edition_specific_types` WRITE;
/*!40000 ALTER TABLE `sheet_music_edition_specific_types` DISABLE KEYS */;
INSERT INTO `sheet_music_edition_specific_types` VALUES ('08dd6ddd-e153-4d02-8580-a5d964ee23cc','Самоучитель игры на русских народных ложках');
/*!40000 ALTER TABLE `sheet_music_edition_specific_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sheet_music_editions`
--

DROP TABLE IF EXISTS `sheet_music_editions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sheet_music_editions` (
  `goods_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `author` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `release_year` int NOT NULL,
  `soft_deleted` tinyint(1) NOT NULL,
  `price` int NOT NULL,
  `status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `specific_type_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `receipt_date` datetime(6) DEFAULT NULL,
  `delivery_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`goods_id`),
  KEY `ix_sheet_music_editions_specific_type_id` (`specific_type_id`),
  KEY `ix_sheet_music_editions_delivery_id` (`delivery_id`),
  CONSTRAINT `fk_sheet_music_editions_goods_delivery_delivery_id` FOREIGN KEY (`delivery_id`) REFERENCES `goods_delivery` (`goods_delivery_id`),
  CONSTRAINT `fk_sheet_music_editions_sheet_music_edition_specific_types_spec` FOREIGN KEY (`specific_type_id`) REFERENCES `sheet_music_edition_specific_types` (`specific_type_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sheet_music_editions`
--

LOCK TABLES `sheet_music_editions` WRITE;
/*!40000 ALTER TABLE `sheet_music_editions` DISABLE KEYS */;
INSERT INTO `sheet_music_editions` VALUES ('08dd6ddd-e155-4fb9-8263-ef8a730441f0','Добрыня Попопич',1800,0,99,'InStock','Напечатан на страницах из деревьев русских да текст на нем высечен чернилами родными да такими что враг посягаться не вздумает на землю русскую','Самоучитель игры на ложках','08dd6ddd-e153-4d02-8580-a5d964ee23cc','2025-03-28 09:49:49.216276','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddd-e158-42af-88cc-83977d2089c9','Добрыня Попопич',1800,0,99,'InStock','Напечатан на страницах из деревьев русских да текст на нем высечен чернилами родными да такими что враг посягаться не вздумает на землю русскую','Самоучитель игры на ложках','08dd6ddd-e153-4d02-8580-a5d964ee23cc','2025-03-28 09:49:49.216276','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddd-e158-42d4-8204-3cc43d3dbc4a','Добрыня Попопич',1800,0,99,'InStock','Напечатан на страницах из деревьев русских да текст на нем высечен чернилами родными да такими что враг посягаться не вздумает на землю русскую','Самоучитель игры на ложках','08dd6ddd-e153-4d02-8580-a5d964ee23cc','2025-03-28 09:49:49.216276','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddd-e158-42f0-8d28-169f57598b1c','Добрыня Попопич',1800,0,99,'InStock','Напечатан на страницах из деревьев русских да текст на нем высечен чернилами родными да такими что враг посягаться не вздумает на землю русскую','Самоучитель игры на ложках','08dd6ddd-e153-4d02-8580-a5d964ee23cc','2025-03-28 09:49:49.216276','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddd-e158-430d-8ef2-7f752ea89ce3','Добрыня Попопич',1800,0,99,'InStock','Напечатан на страницах из деревьев русских да текст на нем высечен чернилами родными да такими что враг посягаться не вздумает на землю русскую','Самоучитель игры на ложках','08dd6ddd-e153-4d02-8580-a5d964ee23cc','2025-03-28 09:49:49.216276','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddd-e158-4325-8c05-eb65540100c8','Добрыня Попопич',1800,0,99,'InStock','Напечатан на страницах из деревьев русских да текст на нем высечен чернилами родными да такими что враг посягаться не вздумает на землю русскую','Самоучитель игры на ложках','08dd6ddd-e153-4d02-8580-a5d964ee23cc','2025-03-28 09:49:49.216276','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddd-e158-434c-88dd-282d87931f0a','Добрыня Попопич',1800,0,99,'InStock','Напечатан на страницах из деревьев русских да текст на нем высечен чернилами родными да такими что враг посягаться не вздумает на землю русскую','Самоучитель игры на ложках','08dd6ddd-e153-4d02-8580-a5d964ee23cc','2025-03-28 09:49:49.216276','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddd-e158-4366-8c13-1971801f6d57','Добрыня Попопич',1800,0,99,'InStock','Напечатан на страницах из деревьев русских да текст на нем высечен чернилами родными да такими что враг посягаться не вздумает на землю русскую','Самоучитель игры на ложках','08dd6ddd-e153-4d02-8580-a5d964ee23cc','2025-03-28 09:49:49.216276','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddd-e158-4379-8489-14611b2ba36e','Добрыня Попопич',1800,0,99,'InStock','Напечатан на страницах из деревьев русских да текст на нем высечен чернилами родными да такими что враг посягаться не вздумает на землю русскую','Самоучитель игры на ложках','08dd6ddd-e153-4d02-8580-a5d964ee23cc','2025-03-28 09:49:49.216276','795843e7-b1b4-4d21-917b-ce15f7fb926a'),('08dd6ddd-e158-4391-8acd-e1446b0fea57','Добрыня Попопич',1800,0,99,'InStock','Напечатан на страницах из деревьев русских да текст на нем высечен чернилами родными да такими что враг посягаться не вздумает на землю русскую','Самоучитель игры на ложках','08dd6ddd-e153-4d02-8580-a5d964ee23cc','2025-03-28 09:49:49.216276','795843e7-b1b4-4d21-917b-ce15f7fb926a');
/*!40000 ALTER TABLE `sheet_music_editions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'musical_shop'
--
/*!50003 DROP FUNCTION IF EXISTS `total_goods_units_count` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `total_goods_units_count`(sale_id char(36)) RETURNS int
    READS SQL DATA
BEGIN
              DECLARE total INT;
              SET total = IFNULL((SELECT COUNT(*) FROM musical_instrument_sale AS mis WHERE mis.sale_id = sale_id), 0);
              SET total = total + IFNULL((SELECT COUNT(*) FROM accessory_sale AS acs WHERE acs.sale_id = sale_id), 0);
              SET total = total + IFNULL((SELECT COUNT(*) FROM sheet_music_edition_sale AS smes WHERE smes.sale_id = sale_id), 0);
              SET total = total + IFNULL((SELECT COUNT(*) FROM audio_equipment_unit_sale AS aeus WHERE aeus.sale_id = sale_id), 0);
              RETURN total;
          END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `total_price` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `total_price`(sale_id char(36)) RETURNS int
    READS SQL DATA
BEGIN
              DECLARE total INT;
              SET total = IFNULL((SELECT SUM(`mi`.`price`) FROM `musical_instrument_sale` AS `mis` LEFT JOIN `musical_instruments` AS `mi` ON `mi`.`goods_id` = `mis`.`musical_instrument_id` WHERE `mis`.`sale_id` = `sale_id`), 0);
              SET total = total + IFNULL((SELECT SUM(`goods`.`price`) FROM `accessory_sale` AS `linking_table` LEFT JOIN `accessories` AS `goods` ON `goods`.`goods_id` = `linking_table`.`accessory_id` WHERE `linking_table`.`sale_id` = `sale_id`), 0);
              SET total = total + IFNULL((SELECT SUM(`goods`.`price`) FROM `sheet_music_edition_sale` AS `linking_table` LEFT JOIN `sheet_music_editions` AS `goods` ON `goods`.`goods_id` = `linking_table`.`sheet_music_edition_id` WHERE `linking_table`.`sale_id` = `sale_id`), 0);
              SET total = total + IFNULL((SELECT SUM(`goods`.`price`) FROM `audio_equipment_unit_sale` AS `linking_table` LEFT JOIN `audio_equipment_units` AS `goods` ON `goods`.`goods_id` = `linking_table`.`audio_equipment_unit_id` WHERE `linking_table`.`sale_id` = `sale_id`), 0);
              RETURN total;
          END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Current Database: `musical_shop`
--

USE `musical_shop`;

--
-- Final view structure for view `sales_view`
--

/*!50001 DROP VIEW IF EXISTS `sales_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `sales_view` AS select `sales`.`sale_id` AS `sale_id`,`sales`.`sale_date` AS `sale_date`,`sales`.`reservation_date` AS `reservation_date`,`sales`.`returning_date` AS `returning_date`,`sales`.`status` AS `status`,`total_price`(`sales`.`sale_id`) AS `total`,`sales`.`paid_by` AS `paid_by`,`total_goods_units_count`(`sales`.`sale_id`) AS `goods_units_count`,`sales`.`is_paid` AS `is_paid` from `sales` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-03-28 13:13:34
