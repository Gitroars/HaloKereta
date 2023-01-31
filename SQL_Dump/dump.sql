-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: vultr-prod-3bfe867e-fd1a-4661-964c-7a93d5c43308-vultr-prod-258c.vultrdb.com    Database: mrtdb
-- ------------------------------------------------------
-- Server version	8.0.30

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
SET @MYSQLDUMP_TEMP_LOG_BIN = @@SESSION.SQL_LOG_BIN;
SET @@SESSION.SQL_LOG_BIN= 0;

--
-- GTID state at the beginning of the backup 
--

SET @@GLOBAL.GTID_PURGED=/*!80000 '+'*/ 'a0c2cd27-7f67-11ed-bcb5-5600043f5cd6:1-747';

--
-- Table structure for table `Admins`
--

DROP TABLE IF EXISTS `Admins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Admins` (
  `admin_id` int NOT NULL AUTO_INCREMENT,
  `full_name` varchar(55) NOT NULL,
  `sex` char(1) DEFAULT NULL,
  `email` varchar(55) NOT NULL,
  `password` varchar(30) NOT NULL,
  `mobile_number` varchar(15) NOT NULL,
  `date_of_birth` date DEFAULT NULL,
  PRIMARY KEY (`admin_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Admins`
--

LOCK TABLES `Admins` WRITE;
/*!40000 ALTER TABLE `Admins` DISABLE KEYS */;
INSERT INTO `Admins` VALUES (2,'Nick Fade','n','nickfade@gmail.com','p90rushb','62648154518643','1989-04-15');
/*!40000 ALTER TABLE `Admins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Payments`
--

DROP TABLE IF EXISTS `Payments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Payments` (
  `payment_id` int NOT NULL,
  `payment_type` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`payment_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Payments`
--

LOCK TABLES `Payments` WRITE;
/*!40000 ALTER TABLE `Payments` DISABLE KEYS */;
INSERT INTO `Payments` VALUES (1,'Dana'),(2,'LinkAja'),(3,'OVO'),(4,'GoPay'),(5,'AstraPay'),(6,'i.saku');
/*!40000 ALTER TABLE `Payments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Routes`
--

DROP TABLE IF EXISTS `Routes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Routes` (
  `route_id` int NOT NULL,
  `station_origin_id` int DEFAULT NULL,
  `station_destination_id` int DEFAULT NULL,
  `route_price` int NOT NULL,
  PRIMARY KEY (`route_id`),
  KEY `station_origin_id` (`station_origin_id`),
  KEY `station_destination_id` (`station_destination_id`),
  CONSTRAINT `Routes_ibfk_1` FOREIGN KEY (`station_origin_id`) REFERENCES `Stations` (`station_id`),
  CONSTRAINT `Routes_ibfk_2` FOREIGN KEY (`station_destination_id`) REFERENCES `Stations` (`station_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Routes`
--

LOCK TABLES `Routes` WRITE;
/*!40000 ALTER TABLE `Routes` DISABLE KEYS */;
INSERT INTO `Routes` VALUES (1,1,2,3500),(2,1,3,5000),(3,1,4,6000),(4,1,5,7000),(5,1,6,8000),(6,1,7,9000),(7,1,8,10000),(8,1,9,11000),(9,1,10,12000),(10,1,11,13000),(11,1,12,14000),(12,1,13,14000),(13,2,1,4000),(14,2,3,4000),(15,2,4,5000),(16,2,5,6000),(17,2,6,7000),(18,2,7,7000),(19,2,8,9000),(20,2,9,9000),(21,2,10,10000),(22,2,11,11000),(23,2,12,12000),(24,2,13,13000),(25,3,1,5000),(26,3,2,4000),(27,3,4,3000),(28,3,5,4000),(29,3,6,5000),(30,3,7,6000),(31,3,8,7000),(32,3,9,8000),(33,3,10,9000),(34,3,11,9000),(35,3,12,10000),(36,3,13,11000),(37,4,1,6000),(38,4,2,5000),(39,4,3,3000),(40,4,5,3000),(41,4,6,4000),(42,4,7,5000),(43,4,8,6000),(44,4,9,7000),(45,4,10,8000),(46,4,11,8000),(47,4,12,9000),(48,4,13,10000),(49,5,1,7000),(50,5,2,6000),(51,5,3,4000),(52,5,4,3000),(53,5,6,3000),(54,5,7,4000),(55,5,8,5000),(56,5,9,6000),(57,5,10,7000),(58,5,11,7000),(59,5,12,8000),(60,5,13,9000),(61,6,1,8000),(62,6,2,7000),(63,6,3,5000),(64,6,4,4000),(65,6,5,3000),(66,6,7,3000),(67,6,8,4000),(68,6,9,5000),(69,6,10,6000),(70,6,11,6000),(71,6,12,7000),(72,6,13,8000),(73,7,1,9000),(74,7,2,7000),(75,7,3,6000),(76,7,4,5000),(77,7,5,4000),(78,7,6,3000),(79,7,8,3000),(80,7,9,4000),(81,7,10,5000),(82,7,11,6000),(83,7,12,7000),(84,7,13,7000),(85,8,1,11000),(86,8,2,9000),(87,8,3,8000),(88,8,4,7000),(89,8,5,6000),(90,8,6,5000),(91,8,7,4000),(92,8,9,3000),(93,8,10,3000),(94,8,11,3000),(95,8,12,4000),(96,8,13,5000),(97,9,1,11000),(98,9,2,9000),(99,9,3,8000),(100,9,4,7000),(111,9,5,6000),(112,9,6,5000),(113,9,7,4000),(114,9,8,3000),(115,9,10,3000),(116,9,11,3000),(117,9,12,4000),(118,9,13,5000),(119,10,1,12000),(120,10,2,10000),(121,10,3,9000),(122,10,4,8000),(123,10,5,7000),(124,10,6,6000),(125,10,7,5000),(126,10,8,4000),(127,10,9,3000),(128,10,11,3000),(129,10,12,3000),(130,10,13,4000),(131,11,1,13000),(132,11,2,11000),(133,11,3,9000),(134,11,4,8000),(135,11,5,7000),(136,11,6,6000),(137,11,7,6000),(138,11,8,4000),(139,11,9,3000),(140,11,10,3000),(141,11,12,3000),(142,11,13,4000),(143,12,1,14000),(144,12,2,12000),(145,12,3,10000),(146,12,4,9000),(147,12,5,8000),(148,12,6,7000),(149,12,7,7000),(150,12,8,5000),(151,12,9,4000),(152,12,10,3000),(153,12,11,3000),(154,12,13,3000),(155,13,1,14000),(156,13,2,13000),(157,13,3,11000),(158,13,4,10000),(159,13,5,9000),(160,13,6,8000),(161,13,7,7000),(162,13,8,6000),(163,13,9,5000),(164,13,10,4000),(165,13,11,4000),(166,13,12,3000);
/*!40000 ALTER TABLE `Routes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Stations`
--

DROP TABLE IF EXISTS `Stations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Stations` (
  `station_id` int NOT NULL,
  `station_name` varchar(25) DEFAULT NULL,
  PRIMARY KEY (`station_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Stations`
--

LOCK TABLES `Stations` WRITE;
/*!40000 ALTER TABLE `Stations` DISABLE KEYS */;
INSERT INTO `Stations` VALUES (1,'Lebak Bulus Grab'),(2,'Fatmawati'),(3,'Cipete Raya'),(4,'Haji Nawi'),(5,'Blok A'),(6,'Blok M BCA'),(7,'Asean'),(8,'Senayan'),(9,'Istora Mandiri'),(10,'Bendungan Hilir'),(11,'Setiabudi Astra'),(12,'Dukuh Atas BNI'),(13,'Bundaran HI');
/*!40000 ALTER TABLE `Stations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TicketStatus`
--

DROP TABLE IF EXISTS `TicketStatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TicketStatus` (
  `ticket_status_id` int NOT NULL,
  `ticket_status_definition` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`ticket_status_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TicketStatus`
--

LOCK TABLES `TicketStatus` WRITE;
/*!40000 ALTER TABLE `TicketStatus` DISABLE KEYS */;
INSERT INTO `TicketStatus` VALUES (1,'New'),(2,'Entrance Scanned'),(3,'Exit Scanned'),(4,'Expired');
/*!40000 ALTER TABLE `TicketStatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Tickets`
--

DROP TABLE IF EXISTS `Tickets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tickets` (
  `ticket_id` int NOT NULL AUTO_INCREMENT,
  `status_id` int DEFAULT NULL,
  `user_id` int DEFAULT NULL,
  `payment_id` int DEFAULT NULL,
  `route_id` int DEFAULT NULL,
  `transaction_date` date DEFAULT NULL,
  `transaction_datetime` datetime DEFAULT NULL,
  PRIMARY KEY (`ticket_id`),
  KEY `user_id` (`user_id`),
  KEY `payment_id` (`payment_id`),
  KEY `route_id` (`route_id`),
  KEY `FK_TicketStatus` (`status_id`),
  CONSTRAINT `FK_TicketStatus` FOREIGN KEY (`status_id`) REFERENCES `TicketStatus` (`ticket_status_id`),
  CONSTRAINT `Tickets_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `Users` (`user_id`),
  CONSTRAINT `Tickets_ibfk_3` FOREIGN KEY (`payment_id`) REFERENCES `Payments` (`payment_id`),
  CONSTRAINT `Tickets_ibfk_4` FOREIGN KEY (`route_id`) REFERENCES `Routes` (`route_id`)
) ENGINE=InnoDB AUTO_INCREMENT=50 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tickets`
--

LOCK TABLES `Tickets` WRITE;
/*!40000 ALTER TABLE `Tickets` DISABLE KEYS */;
INSERT INTO `Tickets` VALUES (46,3,1,2,4,'2023-01-28','2023-01-28 18:31:30'),(47,3,1,1,18,'2023-01-30','2023-01-30 22:36:19'),(48,1,1,6,11,'2023-01-31','2023-01-31 11:01:33'),(49,3,2,2,4,'2023-01-31','2023-01-31 12:02:42');
/*!40000 ALTER TABLE `Tickets` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `full_name` varchar(55) NOT NULL,
  `sex` char(1) DEFAULT NULL,
  `email` varchar(55) NOT NULL,
  `pin` varchar(6) NOT NULL,
  `mobile_number` varchar(15) NOT NULL,
  `date_of_birth` date DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (1,'Marc Kartoz','m','marckartoz@gmail.com','123456','6213151719','1983-07-07'),(2,'Josef Barlow','m','josefbarlow@gmail.com','555888','62987654321','1985-05-25'),(3,'Umi Mayasari','f','nurdiyanti.martaka@zulkarnain.co','133111','6279431674195','1996-02-25'),(4,'Bella Mardhiyah','f','hariyah.talia@gmail.co.id','571311','62874310688','1994-05-24'),(5,'Arta Widodo','m','mandasari.halima@gmail.com','161371','6244068400285','1991-08-24'),(6,'Silvia Kuswandari','f','banawa00@hakim.net','107137','628606155978','1998-01-24'),(7,'Ulva Yuliarti','f','kenzie.yuliarti@waskita.in','220143','6242898756416','1991-08-24'),(8,'Bahuwarna Jailani','m','sitorus.nurul@yahoo.co.id','202112','6243636975098','1998-01-24'),(9,'Asmianto Simanjuntak','m','tfirmansyah@purwanti.web.id','101151','6234470314561','1997-01-25'),(10,'Indah Kusmawati','f','lwaskita@gmail.com','749805','627429651884','1990-09-24');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;
SET @@SESSION.SQL_LOG_BIN = @MYSQLDUMP_TEMP_LOG_BIN;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-01-31 19:57:59
