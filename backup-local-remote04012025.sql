CREATE DATABASE  IF NOT EXISTS `freedb_pickleballcourtbookingsystem` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `freedb_pickleballcourtbookingsystem`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: freedb_pickleballcourtbookingsystem
-- ------------------------------------------------------
-- Server version	8.0.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `address` (
  `id` char(36) NOT NULL,
  `city` varchar(50) DEFAULT NULL,
  `district` varchar(50) DEFAULT NULL,
  `ward` varchar(50) DEFAULT NULL,
  `street` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
INSERT INTO `address` VALUES ('04f1370f-2ba6-471e-8d7b-04b618e01f12','','','',''),('0866eb10-053c-4054-afc2-99cb2841a228','','','',''),('0a4bbf21-3d81-4e85-b8b2-bb4724198820','','','',''),('0b48b0a6-5c44-4cb7-b10a-ecfed2d0126a','','','',''),('11244830-f73b-4ff2-9fa3-b39aabf5fed2','','','',''),('1279a851-ebda-4b9b-880c-aee2068644c8','','','',''),('12bea25d-e946-40d2-b3f5-4843c308cbb7','','','',''),('1524229e-cf27-4f1c-9db0-72e5fc10f4b0','','','',''),('16759957-f579-456c-8680-ddf392ab16d2','','','',''),('1e929d4b-68da-4d08-8ecf-613060df6e13','','','',''),('1ecbb261-6649-451a-8d39-39ffd6ec19f1','','','',''),('2030560a-c3e2-4799-9d68-286e0d9e3329','','','',''),('24e6102f-1669-4a0a-a56f-2d6d8f70be72','','','',''),('266fdfed-dba1-48f2-ba24-95b697a071d9','','','',''),('278f0020-0ceb-461e-8ddd-57380d02170b','Hà Nội','Quận Hoàn Kiếm','Phường Tràng Tiền','Đường Lê Thái Tổ'),('2aa13227-2109-46a4-98df-885bce51b0b8','','','',''),('2adacd2d-b5c5-4c5d-acd4-5916da3c3250','','','',''),('2c080cc4-ed81-45b4-a817-cab948135c18','','','',''),('2cf38824-f8e3-4446-8d88-c2dc5eddace6','','','',''),('2e602fc2-8c16-4322-bbdf-4813d1497ec9','','','',''),('305e3e4c-bb67-4987-935e-ab05bb49c7da','','','',''),('34b87b1d-9d84-42a0-8052-e88ee2ad1d75','','','',''),('38c3bc4e-41de-4a9a-b3e5-1ca643637792','','','',''),('3c622d63-1379-45e9-ba05-93244da63244','','','',''),('3d226dbd-1abe-49a4-810c-c7425dd8326a','','','',''),('3f359814-870e-4b23-b552-8e79981dcaf4','','','',''),('3fa85f64-5717-4562-b3fc-2c963f66afa6','Hồ Chí Minh','Quận 1','Phường Bến Nghé','Đường Nguyễn Huệ'),('42724a1a-b30e-486f-b0cd-6966a2226778','','','',''),('4318077d-4f5a-45dc-b5bd-5a7aa50d3874','','','',''),('444bcb07-0ef8-406e-bd22-530ad5acb781','','','',''),('49868857-65fb-48cc-84a6-1f1a132d4e36','','','',''),('49dba76c-282d-4681-901f-f732803d8650','','','',''),('55193a8c-c66c-4e65-983e-c3736018baf0','','','',''),('55f25b5d-726b-46f6-8352-3790ca08c165','','','',''),('5999e386-8d85-48d8-a5ae-9708f65c2bae','','','',''),('67448100-8b59-4efb-9aa1-7c31b48512d0','','','',''),('6911c310-3424-4b5c-b438-8406a4837925','','','',''),('6a8bf4d5-c068-4f8f-9de2-52ea3eafb1e3','','','',''),('6c2a3e36-de73-4d5d-acca-6dc1cb0cd30f','','','',''),('6f95aff9-b981-4207-833c-fc01535f0b4c','','','',''),('7257bf90-1b09-43f1-824c-7742167a2fdb','','','',''),('73fa48d4-62d7-482e-bbff-a6665f520bf0','','','',''),('764af1d9-64ea-486f-b778-e102233a8d8b','Hà Nội','Quận Ba Đình','Phường Ngọc Khánh','Đường Kim Mã'),('7835b59f-2a15-4470-b225-e56d335102d3','','','',''),('7aa63e17-44be-4565-85b3-2a7b97f4fb13','','','',''),('7bf5a091-cc76-48d8-8599-f55068e78b59','','','',''),('7d6aa2fc-a0df-4a05-bdc6-b08af457c646','','','',''),('88877bb8-a484-415f-9db9-dd065aef1b0f','','','',''),('89a72e7f-8fbf-4c53-bb9f-4fd602d87b76','','','',''),('8a64fdd9-3e2f-43b5-b433-c63efc913c4c','','','',''),('8db111ee-55b1-4951-a282-f818e7a95293','','','',''),('8e4a01a0-4c38-4f6b-bc5e-ac0e1217acc1','','','',''),('956d7092-e0dc-4725-a512-cb30bf3f1db8','','','',''),('97ccb6cb-a839-4305-bcf6-42af06b69500','','','',''),('98385085-0195-415d-b1ab-90adc9239f35','','','',''),('9898ddfd-1f07-4038-8b83-6aa844e0a100','Đà Nẵng','Quận Ngũ Hành Sơn','Phường Khuê Mỹ','Đường Lê Văn Duyệt'),('9be92436-9a7c-45e7-8a87-6b635b6309bf','','','',''),('9c655ff7-f14a-460f-9b39-77ad1f6731e3','','','',''),('9c9a2f04-1a8b-48a5-ae0e-1e50d2a1d10b','','','',''),('9cbe50f0-748e-4166-85fd-e4287ec18411','','','',''),('a1e60b60-d45c-4a8f-add0-143a859da72d','','','',''),('a2457334-18e6-4432-b0d2-9f4d0b2e105f','','','',''),('a257d892-288f-49a3-9224-6e980cb2d262','','','',''),('a5bff1f0-5b8f-4c27-b462-d93907ac7559','Hồ Chí Minh','Quận 3','Phường Võ Thị Sáu','Đường Trường Sa'),('a5d90b9c-a3b5-481b-a7ad-85aa0cfa56e2','','','',''),('a6663929-31cb-44d5-9136-3d029e7ac18a','','','',''),('a96edea3-4cba-4f6f-bbf6-6c0be22912f1','','','',''),('ad7206ce-096a-46b8-9ac8-28c87b7ab7d6','','','',''),('b12cac13-c528-4c0f-8c55-1431f332e16a','','','',''),('b2c5d6e5-b652-4cc6-9212-b893c42fef0f','','','',''),('b3a55de8-b847-4920-9b82-882bd602b595','Khánh Hòa','TP. Nha Trang','Phường Lộc Thọ','Đường Trần Phú'),('b40ce84a-82bb-423f-b77b-fffcac925c0c','','','',''),('b78fd79e-7004-4aa5-8c46-c872c5f84039','','','',''),('bd990a74-21bd-4be7-8fac-0d4799e9fee9','','','',''),('c22e68f6-97f6-4775-aa7b-25935707abe9','','','',''),('c393b3b3-0d43-4090-a597-02f1e2e84ca9','','','',''),('c91304a8-2670-48bf-9bf0-50175fab36da','','','',''),('caef5b56-3e90-40f4-985d-c2e1f19670c5','','','',''),('d53eeab3-97bd-4526-8670-4aac4d6995ba','','','',''),('d88f31c1-80a1-4c26-9775-98541b8abb9b','','','',''),('d8fe6eb7-d60b-42c5-9cf7-789abd25fbda','Hà Nội','Quận Tây Hồ','Phường Quảng An','Đường Âu Cơ'),('dcb8a686-8b39-4f81-b5ce-dfb4c8f52bc2','','','',''),('dd4593d0-26dc-435f-b567-c07d38eccff1','Hòa Bình','TP. Hòa Bình','Phường Đồng Tiến','Đường Lê Lợi'),('e16741a1-5fd3-4b39-9ff5-6e9f51d34e02','','','',''),('e339a5ca-93cd-4fa6-af04-2144d8e0ec39','','','',''),('e4d88a1d-34c3-4269-9c8c-4a82dbae6851','','','',''),('e5023ff0-724c-4d70-a3fb-423eb8a9aa95','','','',''),('e88c4dde-046e-4645-864b-77c1ce633b12','','','',''),('e964d230-ef8e-4f53-b6ae-301dd8835c7b','Hà Nội','Quận Cầu Giấy','Phường Dịch Vọng','Đường Xuân Thủy'),('efa911fb-d5aa-46ba-963c-647364cf146a','','','',''),('efd29050-217a-43a3-875e-4184d5872156','','','',''),('f4df01c4-12a6-434d-9d09-e12b71b830c2','Hồ Chí Minh','Quận 5','Phường 10','Đường Nguyễn Tri Phương'),('f580abb0-e688-4984-8438-6db2a142d4b4','','','',''),('f6c7a5db-f58e-48bc-947a-d0d106214015','','','',''),('f840252e-22cd-4a30-a807-7fe0082db167','','','',''),('fa6bec1a-22de-496a-b1c4-36d546c959c2','','','',''),('ff44a489-ab46-4511-be26-fbc5df3335cd','Hà Nội','Quận Hoàng Mai','Phường Thanh Trì','Đường Tam Trinh');
/*!40000 ALTER TABLE `address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin` (
  `id` char(36) NOT NULL,
  `userId` char(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_admin_userId` (`userId`),
  CONSTRAINT `Admin_ibfk_1` FOREIGN KEY (`userId`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin`
--

LOCK TABLES `admin` WRITE;
/*!40000 ALTER TABLE `admin` DISABLE KEYS */;
/*!40000 ALTER TABLE `admin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `booking`
--

DROP TABLE IF EXISTS `booking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `booking` (
  `id` char(36) NOT NULL,
  `code` varchar(45) DEFAULT NULL,
  `timeBooking` timestamp NULL DEFAULT NULL,
  `amount` double DEFAULT NULL,
  `status` int DEFAULT NULL,
  `paymentStatus` int DEFAULT NULL,
  `courtId` char(36) DEFAULT NULL,
  `customerId` char(36) DEFAULT NULL,
  `courtClusterId` char(36) DEFAULT NULL,
  `courtOwnerId` char(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code_UNIQUE` (`code`),
  KEY `Booking_ibfk_3_idx` (`courtClusterId`),
  KEY `idx_booking_courtId` (`courtId`),
  KEY `idx_booking_courtClusterId` (`courtClusterId`),
  KEY `idx_booking_customerId` (`customerId`),
  KEY `idx_booking_status_paymentStatus` (`status`,`paymentStatus`),
  KEY `Booking_courtowner_fk_idx` (`courtOwnerId`),
  CONSTRAINT `Booking_courtowner_fk` FOREIGN KEY (`courtOwnerId`) REFERENCES `courtowner` (`id`),
  CONSTRAINT `Booking_ibfk_1` FOREIGN KEY (`courtId`) REFERENCES `court` (`id`),
  CONSTRAINT `Booking_ibfk_2` FOREIGN KEY (`customerId`) REFERENCES `customer` (`id`),
  CONSTRAINT `Booking_ibfk_3` FOREIGN KEY (`courtClusterId`) REFERENCES `courtcluster` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `booking`
--

LOCK TABLES `booking` WRITE;
/*!40000 ALTER TABLE `booking` DISABLE KEYS */;
INSERT INTO `booking` VALUES ('132bda98-f332-444b-9930-edd1fced1032','007','2025-01-02 16:01:54',200000,0,0,'f239ab68-1a67-4b35-81b7-619399c0df78','a015c0c0-4c2d-4a23-a3e3-c0b60e2369ca','4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c','816762f5-736d-40e4-9a1d-4d7f2e9ed920'),('18e948e3-4b7f-4032-80f9-01ea02728ad9','009','2025-01-02 16:20:23',150000,0,0,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca','a015c0c0-4c2d-4a23-a3e3-c0b60e2369ca','4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c','816762f5-736d-40e4-9a1d-4d7f2e9ed920'),('38cd4a6a-9e6c-464c-8cf7-b7be645cb93d','005','2025-01-02 15:10:34',150000,1,0,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93','a015c0c0-4c2d-4a23-a3e3-c0b60e2369ca','4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c','816762f5-736d-40e4-9a1d-4d7f2e9ed920'),('be534c35-2815-4a3b-9839-2d43bf091b35','BK0010','2025-01-03 11:30:24',200000,0,0,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93','8a5f8ae2-3d29-4cf4-8bb4-78efdccbf7f3','4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c','816762f5-736d-40e4-9a1d-4d7f2e9ed920'),('f8855e10-cf40-4c45-b3b8-89752aeeac2b','008','2025-01-02 16:04:06',350000,0,0,'f239ab68-1a67-4b35-81b7-619399c0df78','a015c0c0-4c2d-4a23-a3e3-c0b60e2369ca','4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c','816762f5-736d-40e4-9a1d-4d7f2e9ed920');
/*!40000 ALTER TABLE `booking` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cancellation`
--

DROP TABLE IF EXISTS `cancellation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cancellation` (
  `id` char(36) NOT NULL,
  `timeCancel` datetime DEFAULT NULL,
  `bookingId` char(36) DEFAULT NULL,
  `reason` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_cancellation_bookingId` (`bookingId`),
  CONSTRAINT `Cancellation_ibfk_1` FOREIGN KEY (`bookingId`) REFERENCES `booking` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cancellation`
--

LOCK TABLES `cancellation` WRITE;
/*!40000 ALTER TABLE `cancellation` DISABLE KEYS */;
/*!40000 ALTER TABLE `cancellation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `court`
--

DROP TABLE IF EXISTS `court`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `court` (
  `id` char(36) NOT NULL,
  `courtNumber` int DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `courtClusterId` char(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_court_courtClusterId` (`courtClusterId`),
  CONSTRAINT `Court_ibfk_1` FOREIGN KEY (`courtClusterId`) REFERENCES `courtcluster` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `court`
--

LOCK TABLES `court` WRITE;
/*!40000 ALTER TABLE `court` DISABLE KEYS */;
INSERT INTO `court` VALUES ('0a95a426-2eb6-400d-a923-22a7b6012057',1,'Court 1 of Sân Bãi Biển','dc9e3f2e-6e1f-491f-bbe2-1caa86b0ccf1'),('0c85c8f9-b071-4330-bf99-bf3b89039ab6',3,'Court 3 of Sân Cát Vàng','0ff7bd4d-8483-42c5-a29b-7c7996dfcbe5'),('10415d45-33f2-474f-9d24-b6a01033f6ea',2,'Court 2 of Sân Bãi Biển','dc9e3f2e-6e1f-491f-bbe2-1caa86b0ccf1'),('118d597b-1d8f-48f3-baf6-e92564a5e6ad',2,'Court 2 of Sân Cát Vàng','0ff7bd4d-8483-42c5-a29b-7c7996dfcbe5'),('1cbf053f-98d3-4c4b-b83b-0d4ef2c41d43',2,'Court 2 of Sân Lộc Bình','e624fe58-657e-419f-bc76-b219b58986e5'),('1f5d0512-bd5d-46bb-9447-412d0a34ffec',2,'Court 2 of Sân Ánh Dương','3c85fd97-f7eb-4cb1-bc33-22dd9b7e2386'),('253ccae2-b2d5-4b6e-8ca9-f2f20413b6c7',2,'Court 2 of Ocean Breeze Courts','3e618b02-1f69-4855-9e94-c3e96b92abda'),('25c29e59-75c2-4f90-a07a-dfa1f44586b9',3,'Court 3 of Sân Bãi Biển','dc9e3f2e-6e1f-491f-bbe2-1caa86b0ccf1'),('2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca',4,'Court 4 of Golden Pickle Courts','4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('34ab4ea9-bb47-4a07-b58b-bd492872a2fe',2,'Court 2 of Green Valley Courts','aa2d4412-b843-4d57-aec7-18b75c9c68ec'),('3c7bfc95-72bc-43a2-b377-6d8a13d2ca04',4,'Court 4 of Sân Ngọc Lan','8e7f9b8c-fc22-4c77-98c7-7c0e6a8f1b1e'),('45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93',1,'Court 1 of Golden Pickle Courts','4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('48c1a6a2-6e96-4773-b423-86f10e3f83a5',1,'Court 1 of Sân Ánh Dương','3c85fd97-f7eb-4cb1-bc33-22dd9b7e2386'),('55e01362-51f4-4528-9b7b-813d209fb7d1',1,'Court 1 of Green Valley Courts','aa2d4412-b843-4d57-aec7-18b75c9c68ec'),('5b74a6a4-e74d-48de-bb1b-9b3d4a0f8c0a',3,'Court 3 of Sân Ngọc Lan','8e7f9b8c-fc22-4c77-98c7-7c0e6a8f1b1e'),('7743b0ab-b3f6-476d-99a7-b111228b289f',1,'Court 1 of Sân Bình An','4cf6a6d5-52b1-4854-8365-c72015682e9c'),('7db06316-c1cf-4e65-bef7-daf4dbd2c089',3,'Court 3 of Sân Bình An','4cf6a6d5-52b1-4854-8365-c72015682e9c'),('864d69ae-d631-4fe5-b53d-1e7abf39a3c6',4,'Court 4 of Sân Hoàng Gia','76996b27-3e34-4b5c-8a36-9304b2d87e74'),('8e4ec48e-421d-4975-85fe-e5694778c45e',1,'Court 1 of Sân Cát Vàng','0ff7bd4d-8483-42c5-a29b-7c7996dfcbe5'),('9b8f3216-42f5-4501-bde0-24fc93de4597',3,'Court 3 of Sân Hoàng Gia','76996b27-3e34-4b5c-8a36-9304b2d87e74'),('a35712a1-c409-4e0a-b30b-22f68d49b7e0',1,'Court 1 of Sân Hoàng Gia','76996b27-3e34-4b5c-8a36-9304b2d87e74'),('ac75806f-d75a-40e5-a8c4-b62d0708b57f',3,'Court 3 of Green Valley Courts','aa2d4412-b843-4d57-aec7-18b75c9c68ec'),('b27a9ab2-c38f-45b7-9bc7-35d4b0b81915',1,'Court 1 of Sân Ngọc Lan','8e7f9b8c-fc22-4c77-98c7-7c0e6a8f1b1e'),('b792cf6a-3132-4249-a62b-abb80f088302',3,'Court 3 of Sân Ánh Dương','3c85fd97-f7eb-4cb1-bc33-22dd9b7e2386'),('c170fa91-3506-44f2-9a1d-498c93c7f587',3,'Court 3 of Ocean Breeze Courts','3e618b02-1f69-4855-9e94-c3e96b92abda'),('c848e29a-2b98-47e0-bd3d-df88b30e2a92',2,'Court 2 of Golden Pickle Courts','4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('c8cb2f9d-17c7-4b9e-b28b-07e4e62a43c4',2,'Court 2 of Sân Bình An','4cf6a6d5-52b1-4854-8365-c72015682e9c'),('cc66b09e-4d89-4ad0-b0da-70c9bc423f1d',1,'Court 1 of Ocean Breeze Courts','3e618b02-1f69-4855-9e94-c3e96b92abda'),('ce0ac705-b93a-48bc-9b2b-c9f80c687d82',4,'Court 4 of Green Valley Courts','aa2d4412-b843-4d57-aec7-18b75c9c68ec'),('d8531c4a-1a63-4f8d-8f3b-0c1c27c0c0c7',2,'Court 2 of Sân Ngọc Lan','8e7f9b8c-fc22-4c77-98c7-7c0e6a8f1b1e'),('dafc6a3e-b872-42b1-bcba-b3e29c4e52c6',2,'Court 2 of Sân Hoàng Gia','76996b27-3e34-4b5c-8a36-9304b2d87e74'),('dfc676ae-f402-42ac-8e89-0d4d2f7f9c4e',3,'Court 3 of Sân Lộc Bình','e624fe58-657e-419f-bc76-b219b58986e5'),('ed34a8f3-2990-417b-a3e3-c7f315b08371',1,'Court 1 of Sân Lộc Bình','e624fe58-657e-419f-bc76-b219b58986e5'),('ed42bc68-520b-4f65-a727-1a1a04263f2b',4,'Court 4 of Sân Ánh Dương','3c85fd97-f7eb-4cb1-bc33-22dd9b7e2386'),('f239ab68-1a67-4b35-81b7-619399c0df78',3,'Court 3 of Golden Pickle Courts','4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('fa375ba0-bf33-4cc0-8042-c93aeb5c3d47',4,'Court 4 of Ocean Breeze Courts','3e618b02-1f69-4855-9e94-c3e96b92abda');
/*!40000 ALTER TABLE `court` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `courtcluster`
--

DROP TABLE IF EXISTS `courtcluster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `courtcluster` (
  `id` char(36) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `openingTime` time DEFAULT NULL,
  `closingTime` time DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `addressId` char(36) DEFAULT NULL,
  `courtOwnerId` char(36) DEFAULT NULL,
  `status` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_courtCluster_addressId` (`addressId`),
  KEY `idx_courtCluster_courtOwnerId` (`courtOwnerId`),
  CONSTRAINT `CourtCluster_CourtOwnerId__Fk` FOREIGN KEY (`courtOwnerId`) REFERENCES `courtowner` (`id`),
  CONSTRAINT `CourtCluster_ibfk_1` FOREIGN KEY (`addressId`) REFERENCES `address` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `courtcluster`
--

LOCK TABLES `courtcluster` WRITE;
/*!40000 ALTER TABLE `courtcluster` DISABLE KEYS */;
INSERT INTO `courtcluster` VALUES ('0ff7bd4d-8483-42c5-a29b-7c7996dfcbe5','Sân Sao Mai','06:00:00','20:30:00','Cụm sân mới xây với cơ sở vật chất hiện đại, phù hợp cho mọi lứa tuổi và trình độ.','e964d230-ef8e-4f53-b6ae-301dd8835c7b','c6c9d2bc-98e7-4d15-98d8-2c9945a22a91',1),('3c85fd97-f7eb-4cb1-bc33-22dd9b7e2386','Sân Ánh Dương','06:00:00','20:00:00','Cụm sân nổi bật với thiết kế mở, tận dụng tối đa ánh sáng tự nhiên vào ban ngày.','a5bff1f0-5b8f-4c27-b462-d93907ac7559','d7388cb9-c830-41b1-9f27-95aabcb63c36',1),('3e618b02-1f69-4855-9e94-c3e96b92abda','Ocean Breeze Courts','07:00:00','22:00:00','Cụm sân gần biển, không khí trong lành và mát mẻ, mang lại trải nghiệm chơi đầy thư giãn.','b3a55de8-b847-4920-9b82-882bd602b595','0f4c2309-2be8-4f80-b0d1-82c1f3f8d96c',1),('4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c','Golden Pickle Courts','07:00:00','23:00:00','Cụm sân hiện đại với ánh sáng vàng sang trọng, thiết kế phù hợp cho các giải đấu chuyên nghiệp.','278f0020-0ceb-461e-8ddd-57380d02170b','816762f5-736d-40e4-9a1d-4d7f2e9ed920',1),('4cf6a6d5-52b1-4854-8365-c72015682e9c','Sân Bình Minh','05:00:00','19:00:00','Nơi lý tưởng để khởi đầu một ngày mới với không gian thoáng đãng và ánh nắng ban mai.','d8fe6eb7-d60b-42c5-9cf7-789abd25fbda','816762f5-736d-40e4-9a1d-4d7f2e9ed920',1),('76996b27-3e34-4b5c-8a36-9304b2d87e74','Sân Hoàng Gia','08:00:00','20:00:00','Cụm sân cao cấp với dịch vụ đạt chuẩn 5 sao, thích hợp cho các buổi chơi riêng tư và sự kiện cao cấp.','764af1d9-64ea-486f-b778-e102233a8d8b','a97a57a9-4a18-4f09-83bc-2199de84cbd6',1),('8e7f9b8c-fc22-4c77-98c7-7c0e6a8f1b1e','Sân Ngọc Lan','06:00:00','22:00:00','Một cụm sân xanh mát, được bao quanh bởi hàng cây ngọc lan thơm ngát. Phù hợp cho các buổi chơi giải trí và thi đấu nhỏ.','3fa85f64-5717-4562-b3fc-2c963f66afa6','0f4c2309-2be8-4f80-b0d1-82c1f3f8d96c',1),('aa2d4412-b843-4d57-aec7-18b75c9c68ec','Green Valley Courts','05:30:00','21:30:00','Cụm sân nằm trong thung lũng xanh, mang lại cảm giác thoải mái và hòa mình với thiên nhiên.','9898ddfd-1f07-4038-8b83-6aa844e0a100','c6c9d2bc-98e7-4d15-98d8-2c9945a22a91',1),('dc9e3f2e-6e1f-491f-bbe2-1caa86b0ccf1','Mountain View Courts','06:30:00','21:00:00','Cụm sân trên cao với tầm nhìn hướng núi tuyệt đẹp, mang lại cảm giác yên bình và thư thái.','dd4593d0-26dc-435f-b567-c07d38eccff1','a97a57a9-4a18-4f09-83bc-2199de84cbd6',1),('e624fe58-657e-419f-bc76-b219b58986e5','Sân Hoa Anh Đào','05:30:00','21:00:00','Cụm sân mang phong cách Nhật Bản với hàng hoa anh đào xung quanh, tạo không gian lãng mạn và thư giãn.','ff44a489-ab46-4511-be26-fbc5df3335cd','0f4c2309-2be8-4f80-b0d1-82c1f3f8d96c',1),('ec6a3b65-7c1a-46a8-9a67-76f93f931b73','Central Park Courts','06:00:00','22:30:00','Cụm sân tọa lạc ngay trung tâm thành phố, thuận tiện cho việc di chuyển và tổ chức các sự kiện lớn.','f4df01c4-12a6-434d-9d09-e12b71b830c2','d7388cb9-c830-41b1-9f27-95aabcb63c36',1);
/*!40000 ALTER TABLE `courtcluster` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `courtowner`
--

DROP TABLE IF EXISTS `courtowner`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `courtowner` (
  `id` char(36) NOT NULL,
  `userId` char(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_courtOwner_userId` (`userId`),
  CONSTRAINT `CourtOwner_ibfk_1` FOREIGN KEY (`userId`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `courtowner`
--

LOCK TABLES `courtowner` WRITE;
/*!40000 ALTER TABLE `courtowner` DISABLE KEYS */;
INSERT INTO `courtowner` VALUES ('0f4c2309-2be8-4f80-b0d1-82c1f3f8d96c','0f4c2309-2be8-4f80-b0d1-82c1f3f8d96c'),('816762f5-736d-40e4-9a1d-4d7f2e9ed920','816762f5-736d-40e4-9a1d-4d7f2e9ed920'),('a97a57a9-4a18-4f09-83bc-2199de84cbd6','a97a57a9-4a18-4f09-83bc-2199de84cbd6'),('1dca1593-c95c-41f5-8126-46738ba725c2','c61fcbac-2ff0-4716-8bec-0ecb2301e05e'),('c6c9d2bc-98e7-4d15-98d8-2c9945a22a91','c6c9d2bc-98e7-4d15-98d8-2c9945a22a91'),('d7388cb9-c830-41b1-9f27-95aabcb63c36','d7388cb9-c830-41b1-9f27-95aabcb63c36');
/*!40000 ALTER TABLE `courtowner` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `courtprice`
--

DROP TABLE IF EXISTS `courtprice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `courtprice` (
  `id` char(36) NOT NULL,
  `time` time DEFAULT NULL,
  `price` double DEFAULT NULL,
  `courtClusterId` char(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_courtPrice_courtClusterId` (`courtClusterId`),
  CONSTRAINT `CourtPrice_ibfk_1` FOREIGN KEY (`courtClusterId`) REFERENCES `courtcluster` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `courtprice`
--

LOCK TABLES `courtprice` WRITE;
/*!40000 ALTER TABLE `courtprice` DISABLE KEYS */;
INSERT INTO `courtprice` VALUES ('2f6d2ecf-ad75-421e-bb4f-6918de20fd68','13:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('46d0cef4-d2fe-455f-b355-53a13f1ff60d','07:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('60fcfc1b-656d-4de2-8f03-a21b18af98c1','21:00:00',200000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('6ab8168b-aeb7-443e-a4fb-00bb23e8bf8d','06:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('72ab639b-2993-4b36-87f4-54caecb7063e','20:00:00',200000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('741076e8-9b97-4c20-8f23-5132121d49ee','10:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('81419a61-d501-429e-be8f-bb7c2af3f0aa','05:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('8983d186-73e7-45bf-937e-983ce3f5d2fb','12:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('8b800955-5d5b-4766-96c2-c24dbb66545d','22:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('9524511b-365c-4ced-bb17-a235224c0c8e','08:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('9971999c-69c7-4c69-aef9-1fee9dbb042d','18:00:00',200000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('a0788fde-0586-4098-9217-d55c9777c447','19:00:00',200000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('a8442805-c7b0-43f2-97e8-2bab093d701f','15:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('ca893979-0836-4a63-b649-7abe46c60e93','09:00:00',200000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('d5b7105b-d494-44c2-9f99-a2f85fbb0421','14:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('d8690496-2a31-4128-b380-27a751df7b57','17:00:00',200000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('e554bcd7-17b9-4698-abb0-be2095922564','11:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c'),('fd6a82a4-cffb-465a-80e2-554bf9ad6ce6','16:00:00',150000,'4bfaaf62-88dc-4e8c-abc4-2df7a9e0836c');
/*!40000 ALTER TABLE `courtprice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `courttimebooking`
--

DROP TABLE IF EXISTS `courttimebooking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `courttimebooking` (
  `id` char(36) NOT NULL,
  `bookingId` char(36) DEFAULT NULL,
  `courtTimeSlotId` char(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_courtTimeBooking_bookingId` (`bookingId`),
  KEY `idx_courtTimeBooking_courtTimeSlotId` (`courtTimeSlotId`),
  CONSTRAINT `CourtTimeBooking_ibfk_1` FOREIGN KEY (`bookingId`) REFERENCES `booking` (`id`),
  CONSTRAINT `CourtTimeBooking_ibfk_2` FOREIGN KEY (`courtTimeSlotId`) REFERENCES `courttimeslot` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `courttimebooking`
--

LOCK TABLES `courttimebooking` WRITE;
/*!40000 ALTER TABLE `courttimebooking` DISABLE KEYS */;
INSERT INTO `courttimebooking` VALUES ('36745c61-3e64-46b3-8524-8436e01f04bd','f8855e10-cf40-4c45-b3b8-89752aeeac2b','af46e7b1-c735-4929-8b38-168a2d00ae97'),('44ca9b9d-c0be-47b0-bb28-c8e1800193b7','f8855e10-cf40-4c45-b3b8-89752aeeac2b','af136464-dc3b-4439-a2aa-8dfe21fd4fdc'),('4f79f6d8-6033-4fbd-b9d9-6d53b9ec58ba','be534c35-2815-4a3b-9839-2d43bf091b35','0655eadc-ae4c-4946-aeae-210286ae0a18'),('70d5cf02-897b-4a79-82cd-495ffb83dd13','132bda98-f332-444b-9930-edd1fced1032','019edb8f-5cd6-4078-b23d-84eb416cca0e'),('822ff930-ea88-4755-b26d-74d2be4fd432','38cd4a6a-9e6c-464c-8cf7-b7be645cb93d','005e6492-81ad-48a3-b545-6642744b997c'),('e1d20381-d35d-46ff-b9a2-28873fc6a404','18e948e3-4b7f-4032-80f9-01ea02728ad9','8511d2b8-93e7-48f1-9a41-f8a9f847c2d0');
/*!40000 ALTER TABLE `courttimebooking` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `courttimeslot`
--

DROP TABLE IF EXISTS `courttimeslot`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `courttimeslot` (
  `id` char(36) NOT NULL,
  `date` date DEFAULT NULL,
  `time` time DEFAULT NULL,
  `isAvailable` int DEFAULT NULL,
  `price` double DEFAULT NULL,
  `courtId` char(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_combined` (`courtId`,`isAvailable`,`date`,`time`),
  KEY `idx_isAvailable_date_time` (`isAvailable`,`date`,`time`),
  CONSTRAINT `CourtTimeSlot_ibfk_1` FOREIGN KEY (`courtId`) REFERENCES `court` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `courttimeslot`
--

LOCK TABLES `courttimeslot` WRITE;
/*!40000 ALTER TABLE `courttimeslot` DISABLE KEYS */;
INSERT INTO `courttimeslot` VALUES ('005e6492-81ad-48a3-b545-6642744b997c','2025-01-03','13:00:00',0,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('019edb8f-5cd6-4078-b23d-84eb416cca0e','2025-01-03','09:00:00',0,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('0351d942-f391-44d3-be7c-25562ba45140','2025-01-04','11:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('05359c9c-cdb7-4854-8b07-2ce912633c1b','2025-01-05','08:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('0655eadc-ae4c-4946-aeae-210286ae0a18','2025-01-03','19:00:00',0,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('066e1f83-3dd4-4c1b-b108-e69c47de1abc','2025-01-05','20:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('0c82e5a3-47f7-48d2-b914-82a3feb9560f','2025-01-03','21:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('0c94e05d-726b-4b06-8bc3-a4385a0ad310','2025-01-04','07:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('0df2b7fd-f8c2-4ed2-8a75-e04a90e2062a','2025-01-03','05:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('0eb37a6a-a897-45db-9888-941982d0978f','2025-01-03','10:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('10809fb7-035e-402c-bb7f-79e87f187098','2025-01-04','21:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('11f0a3de-3130-41cf-b439-032671d4aeb2','2025-01-05','07:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('129cf082-2910-4d8e-8e52-45074f6bc64a','2025-01-03','22:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('12e39fd7-0d31-4672-ba7a-87304d361d0e','2025-01-03','06:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('141a735a-4321-4606-99a8-f89f894854cc','2025-01-03','19:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('1485e647-8aa2-453e-a9f2-9f04ddde374b','2025-01-05','11:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('159adfbf-c092-499c-a6d1-c824203d2c33','2025-01-05','19:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('16e5d649-3e4c-4293-9929-3a5901fad53d','2025-01-03','05:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('178feba4-52cb-45f3-a7ce-3d3ef600a11d','2025-01-03','17:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('1ae5cf45-d46a-45a2-bc4a-b6a4baf857b3','2025-01-04','20:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('1d9792b6-d418-4055-ac57-eadf3cbcf705','2025-01-03','22:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('1dabd917-3f4a-4dff-8764-c9877dd18444','2025-01-03','15:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('1eb12ca3-5bc2-45b9-8b47-7d820223a7a4','2025-01-05','16:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('1f342ff1-2e90-4eea-9813-5d0323c222de','2025-01-05','06:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('2094ca49-dea3-46b0-b8c4-8a4fc92902df','2025-01-05','22:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('22219360-cb2e-46c6-ae44-8904dcb77f05','2025-01-05','20:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('2298cd53-3971-4778-9e67-f0bffdc13bc5','2025-01-04','08:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('22a2e0ce-21a6-435a-8d4a-e9f075013e4e','2025-01-05','20:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('22bd80f1-a64e-4330-bfe4-335257ece78e','2025-01-03','08:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('2499f402-a590-437f-8dc9-6ca2c2ef824c','2025-01-04','18:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('24d08be6-923b-4518-afd2-651322e0aa05','2025-01-03','12:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('259e2c0b-df18-4122-acad-b39c8cb6896d','2025-01-05','10:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('2687a2fc-dca8-462c-8bee-b1af23d24d83','2025-01-04','19:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('2745182f-ced9-4090-bcfc-84a777d689c7','2025-01-03','21:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('285d93e0-1551-457b-ab72-48b9dbba37b1','2025-01-05','13:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('292ce52c-f070-4e39-aa24-d7d364674f9c','2025-01-03','22:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('29b6b427-60fd-4d41-84d9-ac0d525819b4','2025-01-05','05:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('2a5f1689-64e8-4ed9-aa5d-a27d74728c02','2025-01-04','09:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('2bcfc878-eb91-462b-b717-126cc15e8e53','2025-01-03','11:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('2c249b10-c711-49ae-9c0f-2f55fa7ecdd1','2025-01-04','15:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('2cd8c5a9-cc07-415a-912c-a16a7a4c1988','2025-01-05','12:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('2e192f45-7f69-485d-bf52-37d68c3d3812','2025-01-05','05:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('2e6f874f-c533-4e00-bdd9-8f07b77ca4cb','2025-01-03','08:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('2ee204b6-8ece-4d60-bd53-8d9ea1708661','2025-01-05','09:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('2eea70b8-a0ec-4935-97dd-c971d6fe2c80','2025-01-03','13:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('346a7b83-7b35-43d0-89b7-caf9f61a2af2','2025-01-05','15:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('34a7018b-4590-4a81-a3b7-cd94f041b15c','2025-01-04','13:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('371b2df2-e86f-4de5-8eb7-3627aac26e04','2025-01-04','06:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('37630617-7881-4ff8-bfe0-3980ad722052','2025-01-04','05:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('37f75560-cb46-480a-ac1b-2961d315fd5a','2025-01-05','18:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('38f914cc-ef87-496f-a967-53a53bc6300a','2025-01-04','14:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('3a176390-9e32-4507-8144-373aad9ae772','2025-01-04','16:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('3c97da13-efe2-4f19-9907-69997fb3d52e','2025-01-04','22:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('3cf1f859-ca73-48f4-af9a-4d13de630032','2025-01-03','15:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('3e94365d-53ba-4d90-8980-33c88d17894d','2025-01-05','14:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('3f8bb520-712e-4202-a63d-5db82a151af7','2025-01-05','22:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('3fcb815e-04ba-4ebe-997d-d695f12025a7','2025-01-03','07:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('3feb39ff-a997-445b-8c9c-9255e2a260e4','2025-01-05','18:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('40c26584-20a7-446c-9528-5728853e56d7','2025-01-05','17:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('425e67b2-1406-4656-b886-30e1275b4ba7','2025-01-05','14:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('44452a8d-80b3-4cd3-93a5-0ad86b5de9bf','2025-01-05','17:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('44c13815-81b6-420b-81dd-51160772cbb1','2025-01-04','21:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('44c258cd-1daa-4bc4-912f-a34de20d24d4','2025-01-05','21:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('4511a5b4-30ef-4ad0-ba74-a538f621d68b','2025-01-04','20:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('46d640bc-bf08-45e8-81ea-e925e19fe554','2025-01-05','05:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('47d6ba06-da40-45d7-b6af-e545e9eb683f','2025-01-05','12:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('484429f6-bef0-482d-9e25-1c0eb91942eb','2025-01-05','14:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('488a15eb-ba53-45c9-b228-5a16a45be739','2025-01-03','09:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('4cd68be9-1f6a-4286-8bbc-49343769088f','2025-01-05','19:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('4f91d3f3-5253-4623-8c1e-37718cfb73d7','2025-01-04','16:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('50fdbe4a-1dc6-4022-a18b-be2e100d3a93','2025-01-03','08:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('51757107-9a37-4794-939f-a16f058cf272','2025-01-03','16:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('54dd287e-01c9-4113-bd7f-99e2cf7952cb','2025-01-04','16:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('55d598b1-4688-4ade-8800-57a4f025aaee','2025-01-03','21:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('55d64e10-5ac3-44be-b840-f8655b40f680','2025-01-03','11:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('56621856-7f10-4e55-9d57-d98dd997a511','2025-01-05','16:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('56fa7d7d-f472-41f8-ad6e-154d5fceb0b6','2025-01-03','09:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('57b5555e-e4b7-4e21-a2b8-6dd8d1455a2f','2025-01-04','22:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('5bfe135e-7a5d-4f0a-ad66-e3520987447f','2025-01-05','10:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('5d1c0a3b-3e1d-4718-8d5f-904252d4900f','2025-01-04','17:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('5e201dac-3a8c-47c5-afb5-2785d7e9043d','2025-01-03','10:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('5f0cd90b-e47e-4ee0-8cac-8c88c4a4a337','2025-01-03','15:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('5f4cca7f-3863-46d3-ae79-6aefbec14e02','2025-01-03','18:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('61ae7129-47b5-4dff-a06e-36e1e801a6ca','2025-01-03','12:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('61c762e6-b19d-4d98-8ae4-f3f74bd42701','2025-01-05','15:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('638d52d3-504b-4e31-9bf4-96408f6d2920','2025-01-03','20:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('649d76e9-559d-43b7-89a4-20eaaef95f85','2025-01-04','07:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('65501e75-c870-41d7-ba4f-dc03a2408c88','2025-01-04','21:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('65947a56-4dbb-4ddd-b626-ce1fdc4dd51c','2025-01-04','15:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('69818b38-1527-458c-85b2-772a6bd26379','2025-01-05','15:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('6d99b26d-a4dc-446d-9b08-7a2803ca427d','2025-01-04','19:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('6ee784b9-0c64-4512-827f-de5608e02907','2025-01-04','20:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('759bb593-07a6-4722-a7ac-0a7d24b2dda4','2025-01-05','22:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('75facc9c-e44e-4bfb-8634-72df0d97238e','2025-01-03','16:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('769a5515-f135-4609-897f-393924227bc1','2025-01-04','05:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('76a2c98d-2995-4cc4-be9e-265be55a6860','2025-01-04','21:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('7765ab00-44e8-49f7-85ff-9882f1f40bb8','2025-01-04','09:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('7833a73b-4fd8-46a8-a6f9-0b1933de0fc8','2025-01-04','18:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('796a17b0-6e94-4361-9658-add09d33a047','2025-01-04','07:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('7a0e455a-43bf-4463-b5e2-cdf03944ff1b','2025-01-03','18:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('7a4a3ff9-1f0c-404e-8896-2753390bf5b9','2025-01-03','14:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('7a4bb3e6-94a4-476b-9c8a-fcb802655237','2025-01-05','07:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('7ac1a4bf-bb61-4e4c-bf30-8cce2a6dfaa8','2025-01-04','20:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('7b3bcd3d-a7e0-4072-96d1-419495e0a274','2025-01-04','10:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('7bef73ed-6ba3-4067-8a8b-4950fae7f284','2025-01-03','08:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('7c1d0338-df5e-4ff2-a74e-ca477067fc48','2025-01-04','05:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('7c236437-e504-4d0a-8339-d47d84824721','2025-01-04','14:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('828685cf-d9e4-4e0c-ba92-ba23e66b21dd','2025-01-05','06:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('82e898e1-343b-4f0b-b6d3-b60a2feb4da5','2025-01-04','10:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('83b27211-23d6-4e5a-9826-0d14124b36b8','2025-01-04','09:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('8511d2b8-93e7-48f1-9a41-f8a9f847c2d0','2025-01-04','11:00:00',0,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('8728bda3-2c5b-4252-8893-32c0b0331b69','2025-01-05','09:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('8840d3d3-c9f4-42ad-8443-11301c9f8602','2025-01-04','10:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('884f5115-2890-44e1-a323-71adef90e661','2025-01-04','17:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('88fff514-2a5e-483f-b079-c7b1bc208a85','2025-01-05','18:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('892c9287-cb61-47d3-87d1-acbae6eaf60f','2025-01-04','18:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('897904ee-663d-4cf6-8389-1514f0835d15','2025-01-03','15:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('8c516cb5-3dfe-4bde-8c96-4f75359937c2','2025-01-03','22:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('8d31eec4-6f25-4ade-acc1-15ab7d119a1b','2025-01-04','11:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('8db29de5-5b0b-4f32-9b9c-4cc6ab921dab','2025-01-05','12:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('8e51c959-ad68-4485-8b3f-97aeb86fcc2c','2025-01-04','12:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('8f91bde1-8a5d-42e4-a7d8-fee792895ab1','2025-01-04','06:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('902a4b29-2658-4e63-8b2c-dab087c6de0a','2025-01-05','11:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('914d8c94-04d2-4e3f-a37e-a41401db1952','2025-01-05','16:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('92d098c6-604a-4521-812e-a8f45e53d258','2025-01-04','13:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('933c4501-85be-4aa0-b595-4452c8f5606d','2025-01-03','17:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('943f7af3-a43e-4d5f-baca-8a3fc707278c','2025-01-05','19:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('9703d940-2f16-4978-9d73-149fe8771350','2025-01-04','11:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('98c15f80-517d-47f0-b74c-83bcc9489f3e','2025-01-05','10:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('990f28f1-eab0-4c9d-8d8d-5d7ecd96f102','2025-01-03','12:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('995ea325-cdac-44ff-ab8b-f89b7be653f7','2025-01-04','07:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('99e4934f-39b8-421c-818b-87603273f447','2025-01-05','16:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('9b28bc61-e25e-4f18-a478-4a1a00883195','2025-01-04','17:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('9e9024e1-93be-4a7e-83df-a7b5ec8a64e4','2025-01-05','11:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('9ea8beb0-8024-49a4-8632-c4d747c94ead','2025-01-03','14:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('9f137bd7-43ae-44f3-89c1-38b0b6336476','2025-01-03','14:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('9f3648d1-e50f-403d-b829-78c817bc7ee3','2025-01-03','13:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('9fa9f498-6da6-436b-8099-a3b7b70e4ccd','2025-01-04','08:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('a460aad5-b5a9-46a7-a0ec-93206f48fca7','2025-01-04','13:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('a49c73d5-034a-459a-9f82-53b082650506','2025-01-03','21:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('a4c35d86-6dd0-4a36-afc1-3613855d2fe5','2025-01-03','14:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('a4dfc6f5-fc1a-418c-85f2-6423d8f376a1','2025-01-03','18:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('a5582d50-5457-486a-9cb8-61f3b141f4fb','2025-01-03','20:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('a57cfeb9-7d62-4db6-a514-e62af3150122','2025-01-05','07:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('a71ec072-df16-4978-9c26-bf9f5238f769','2025-01-03','06:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('a8c8ee78-b7c2-4c50-8caf-655315068189','2025-01-05','13:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('aadcff13-3775-404d-ab0f-b38feefe494c','2025-01-03','11:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('ad85909b-dd71-4cea-a044-5effb0e2b821','2025-01-05','13:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('ade4fa1f-c9a5-406c-aef3-1f89d20f51b7','2025-01-03','16:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('af136464-dc3b-4439-a2aa-8dfe21fd4fdc','2025-01-03','19:00:00',0,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('af46e7b1-c735-4929-8b38-168a2d00ae97','2025-01-03','05:00:00',0,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('b249589c-df8b-4185-8204-341c933a253c','2025-01-03','07:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('b34e6fe7-f034-4b92-b581-da3b5fe45257','2025-01-05','06:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('b4e0f787-0f49-4a88-9ff5-84f35fc4145c','2025-01-03','16:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('b6eafdae-db14-457b-a70a-77358b3f8454','2025-01-05','07:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('b78ee5b6-957a-4743-b011-971e04ff79ef','2025-01-03','09:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('b8134d24-ed27-4dbc-97e5-b140a6b5e3d6','2025-01-03','10:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('bc8537e7-3d9d-4c9f-a7b3-9eb67338f34a','2025-01-05','12:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('be702012-aef1-45ab-aec5-448ffcdf67ad','2025-01-05','17:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('beab0a55-36b5-4735-a1ce-792836748fb8','2025-01-04','22:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('c0dfc02c-21a6-4e01-bb51-0fe2787ee2ce','2025-01-05','21:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('c14613e2-b0d9-4d45-9da4-a9250a43a627','2025-01-03','19:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('c1cd2ca9-e91e-4f20-ad36-5d15c230c2bf','2025-01-03','18:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('c29474a2-0dba-430c-ab26-4989b5005a35','2025-01-04','05:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('c2ae9d44-e5d6-4604-827c-55f1f823ddeb','2025-01-03','06:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('c780a273-6777-4306-b30a-13f5e91647a3','2025-01-04','06:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('c78884bd-56c9-4fae-9b56-2c18211b584c','2025-01-03','12:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('c8982da4-60c6-47ed-beb9-644b7f2e76bb','2025-01-04','22:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('c9bf4807-648f-431c-ac1c-25923a6e7f4c','2025-01-04','12:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('ca1b6917-3f3b-49f2-8d09-3344cc11e24d','2025-01-04','08:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('caf955e0-0fb7-4258-a30b-8df1d77713d2','2025-01-05','18:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('cb5ba8e0-71a4-44f1-bafe-57e0861b3499','2025-01-04','19:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('ce08b6e1-50e7-48f5-84c2-5d0fd3d3a3e3','2025-01-03','13:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('cec0ea02-2be3-4a85-8653-b78bae5f908c','2025-01-03','17:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('cf68e898-68c2-44a4-9228-2bb94d2a3fa5','2025-01-03','05:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('d0bb8345-2cd5-4601-8e32-13929f8a402c','2025-01-04','12:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('d18e4df1-8d9c-4f16-afe8-344816ede8f2','2025-01-05','09:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('d1d9dd52-98cd-45fb-9cb5-c32b6d9f1879','2025-01-05','08:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('d3f36d42-7841-4c1a-b120-0db10ccd4429','2025-01-05','06:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('d4e62037-6ad4-48ee-9979-f01a1ec670b7','2025-01-05','13:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('d545bd8b-a86c-47b7-8ee6-b4c635259b22','2025-01-03','17:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('d7993bed-c08f-45e9-b246-a48697080440','2025-01-05','05:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('d7c5f024-db95-4846-9062-91f451d48311','2025-01-05','21:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('d882a4ed-b65d-4988-ba2b-7a2b1673d5c9','2025-01-04','19:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('d8ddd222-7286-492c-8ef1-ef583e95b177','2025-01-04','09:00:00',1,200000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('d906cd9f-732f-41de-bb2f-9788d644f7b6','2025-01-05','20:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('d94a6b0a-6d03-4d9e-bddd-a0cb6f7f5af1','2025-01-04','14:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('dbea97ad-d5ce-46e8-ad2b-523ad92839b9','2025-01-04','17:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('dbf65e3e-4037-4b27-bb83-3a16e04e73ed','2025-01-04','15:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('dc90cbe5-2712-4b06-8180-8549d946e3c3','2025-01-05','08:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('dd00e457-8281-4495-9b39-43012506a29d','2025-01-03','11:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('ddca9791-2844-4cbb-9b5d-bb705ec1e7a0','2025-01-05','15:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('de79e5fb-a39b-4f49-aa57-43fd5607a26e','2025-01-03','20:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('e4d418ed-19f2-420b-aed4-e068e10ee0f3','2025-01-05','21:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('e6bfa9a1-dd17-4b83-95f8-ab751bd03294','2025-01-05','17:00:00',1,200000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('e7bd7ba7-0aae-47d0-90b8-8d412b9e459d','2025-01-05','14:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('ea0c45f3-a3b6-4b0e-9175-397596192a86','2025-01-04','18:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('ebff7758-02d5-4f54-8a65-3649f2ea298e','2025-01-05','08:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('ed525344-d4ef-4654-93bc-f11f2e65517e','2025-01-04','16:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('f064948d-41a0-4da8-98a3-73bf0dc9971c','2025-01-05','09:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('f09a3d62-4065-4238-96fd-5e2ac0290122','2025-01-03','10:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('f221036e-2546-415d-a908-415ecc08bc06','2025-01-04','08:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('f2ef0215-f135-476d-bac8-054caef4b51d','2025-01-03','20:00:00',1,200000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('f34d2de7-ee98-42b6-bfbf-13d145db3126','2025-01-03','07:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('f4935040-32f0-4d66-9f8f-b2831c9cf5e8','2025-01-04','13:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('f5759938-c251-4026-9837-36e3fb4b42ba','2025-01-04','12:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('f5a8a5b5-d23f-41f7-afc8-87ce7d13b5ea','2025-01-04','15:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('f73dbd7f-17d9-4a66-9889-d8dfc41e6128','2025-01-03','07:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('f9089652-1540-47ba-b560-5a2cbc36c0dd','2025-01-03','06:00:00',1,150000,'45bc27cf-3e3e-4d2d-9537-3fdfe6ac3e93'),('fa4dc5f6-d48e-4447-9b16-01b1d26ab31d','2025-01-05','10:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('fae7be9b-b900-4f0a-9eae-7f1e44edc081','2025-01-04','06:00:00',1,150000,'f239ab68-1a67-4b35-81b7-619399c0df78'),('fb0120cc-1c6f-451f-9a69-709bf6435f8d','2025-01-04','14:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('fb1e2ac7-fa1e-4d75-8fd6-cdeabe188178','2025-01-04','10:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92'),('fbb18165-d0ad-409d-8f3c-302b01fd47ec','2025-01-05','11:00:00',1,150000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('fe8c3f05-9242-4b1a-8684-e76e49deca78','2025-01-05','19:00:00',1,200000,'2cf170f9-fbb4-4c29-bb3d-00fe5725a8ca'),('ff2690a6-9c59-4a17-ad29-1ed276c0fee8','2025-01-05','22:00:00',1,150000,'c848e29a-2b98-47e0-bd3d-df88b30e2a92');
/*!40000 ALTER TABLE `courttimeslot` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customer` (
  `id` char(36) NOT NULL,
  `userId` char(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_customer_userId` (`userId`),
  CONSTRAINT `Customer_ibfk_1` FOREIGN KEY (`userId`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer`
--

LOCK TABLES `customer` WRITE;
/*!40000 ALTER TABLE `customer` DISABLE KEYS */;
INSERT INTO `customer` VALUES ('65105ee6-e308-49f0-aa50-53451aa1f7c0','06f53092-d9cc-4a39-8942-f71ef5a608d9'),('82143454-970c-4643-9561-a00070e13ad2','24484034-b2d1-4786-a73b-7c2e874f699d'),('c865d0c9-bd1f-4910-b842-17fdff2e7181','39f97ae8-3a96-4210-b253-98f2fa89ec21'),('41dcf5c3-ef13-4e2a-9bc5-8447de8facdb','3d23de1e-6bce-4e61-b228-7a32942b51ae'),('397732d5-b57f-4ecb-bde3-da82e26c49fc','49aa59b0-5a4b-4828-91f2-7d7d752445ab'),('c987a708-8232-4b4e-ab40-8d1e0e3b9b7c','4e5eddf4-3d28-44ce-bd7c-88cdc4bbbb8c'),('003ad5c8-31eb-4e30-b938-82d4fdf0c758','52928f32-7443-4d09-92e9-d9f69c0b9c50'),('675cbdff-6d85-4784-bd9e-5dba4848acd3','6005102f-ea47-4d7a-8718-b6ad64a9c32e'),('23cae705-1734-49c6-b6bc-0c8be57f35a3','66de6cf9-15b3-455a-8049-4d52aad27d16'),('b40e2ed9-0f58-4091-8986-efd41ca5b647','6d5db001-7767-469a-aad6-3249a4254571'),('215d06ef-7403-4886-8437-4c31dace3130','725c08ca-9b41-4447-9ebc-65267da75ee3'),('fc4aaf4a-7e2d-4f9f-934e-cf7da5a167f4','9c985bdb-f84e-497a-91af-75ce0022b797'),('54aa9bc9-cb12-4a8c-8de9-6a22221a5270','a4b539af-9f25-4a3e-95a7-2475b37148c4'),('d32066f8-0589-49af-af84-c5746015a1a2','aa20fe8d-9d88-4eed-925e-05541416f290'),('c173d676-0790-404a-8bd3-a99b8f753ea3','c3f96a4d-1686-4667-b296-f2ba7d8f67cf'),('8a5f8ae2-3d29-4cf4-8bb4-78efdccbf7f3','c616da98-3a3b-4726-b7e0-654bddf64275'),('a015c0c0-4c2d-4a23-a3e3-c0b60e2369ca','cd10bf5c-b49b-458a-836d-9837e0570018'),('620d5f72-e6c2-4b18-a0ba-35390df9befa','e0ccdb1c-bd24-48fd-9f63-9d5f912c5c49'),('88ebaf75-0ef4-418c-aeee-c31441a6f5ec','f3d57a09-916e-490b-8a69-8856972fe2d7'),('e7b6336e-2489-4b90-96bb-c69fee3ad537','faa8a081-148a-4c53-b767-305d955a3181');
/*!40000 ALTER TABLE `customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `feedback`
--

DROP TABLE IF EXISTS `feedback`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `feedback` (
  `id` char(36) NOT NULL,
  `rating` float DEFAULT NULL,
  `comment` varchar(100) DEFAULT NULL,
  `courtClusterId` char(36) DEFAULT NULL,
  `bookingId` char(36) DEFAULT NULL,
  `customerId` char(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_feedback_bookingId` (`bookingId`),
  KEY `idx_feedback_courtClusterId` (`courtClusterId`),
  KEY `idx_feedback_customerId` (`customerId`),
  KEY `idx_feedback_rating` (`rating`),
  CONSTRAINT `Feedback_ibfk_1` FOREIGN KEY (`bookingId`) REFERENCES `booking` (`id`),
  CONSTRAINT `Feedback_ibfk_2` FOREIGN KEY (`courtClusterId`) REFERENCES `courtcluster` (`id`),
  CONSTRAINT `Feedback_ibfk_3` FOREIGN KEY (`customerId`) REFERENCES `customer` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `feedback`
--

LOCK TABLES `feedback` WRITE;
/*!40000 ALTER TABLE `feedback` DISABLE KEYS */;
/*!40000 ALTER TABLE `feedback` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `imagecourturl`
--

DROP TABLE IF EXISTS `imagecourturl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `imagecourturl` (
  `id` char(36) NOT NULL,
  `url` varchar(255) DEFAULT NULL,
  `courtClusterId` char(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_imageCourtURL_courtClusterId` (`courtClusterId`),
  CONSTRAINT `ImageCourtURL_ibfk_1` FOREIGN KEY (`courtClusterId`) REFERENCES `courtcluster` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagecourturl`
--

LOCK TABLES `imagecourturl` WRITE;
/*!40000 ALTER TABLE `imagecourturl` DISABLE KEYS */;
/*!40000 ALTER TABLE `imagecourturl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `id` int NOT NULL,
  `roleName` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Admin'),(2,'Customer'),(3,'CourtOwner');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id` char(36) NOT NULL,
  `code` int DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `phoneNumber` varchar(25) DEFAULT NULL,
  `isActive` int DEFAULT NULL,
  `avatarUrl` varchar(255) DEFAULT NULL,
  `addressId` char(36) DEFAULT NULL,
  `roleId` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`),
  UNIQUE KEY `username` (`username`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  UNIQUE KEY `phoneNumber_UNIQUE` (`phoneNumber`),
  KEY `User_ibfk_2_idx` (`roleId`),
  KEY `idx_user_roleId_isActive` (`roleId`,`isActive`),
  KEY `User_addressid_fk_idx` (`addressId`),
  CONSTRAINT `User_addressid_fk` FOREIGN KEY (`addressId`) REFERENCES `address` (`id`),
  CONSTRAINT `User_ibfk_2` FOREIGN KEY (`roleId`) REFERENCES `role` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES ('06f53092-d9cc-4a39-8942-f71ef5a608d9',17,'techmaster','abc123','Tamara Mcfarland','tamaramcfarland@google.ca','08764261045',1,'','2e602fc2-8c16-4322-bbdf-4813d1497ec9',3),('0f4c2309-2be8-4f80-b0d1-82c1f3f8d96c',24,'chusan5','abc123','Seth Morrow','sethmorrow1161@protonmail.edu','03115774806',1,'','8e4a01a0-4c38-4f6b-bc5e-ac0e1217acc1',2),('24484034-b2d1-4786-a73b-7c2e874f699d',18,'mc_coder','abc123','Evangeline Raymond','evangelineraymond@protonmail.ca','05244654321',1,'','c393b3b3-0d43-4090-a597-02f1e2e84ca9',3),('39f97ae8-3a96-4210-b253-98f2fa89ec21',19,'mc_software','abc123','Byron Parsons','byronparsons1880@google.net','03167085137',1,'','e88c4dde-046e-4645-864b-77c1ce633b12',3),('3d23de1e-6bce-4e61-b228-7a32942b51ae',3,'customer4','abc123','Sophia Brown','sophiabrown45@example.com','0934567890',1,'','67448100-8b59-4efb-9aa1-7c31b48512d0',3),('49aa59b0-5a4b-4828-91f2-7d7d752445ab',9,'michael123','abc123','Aimee Murphy','aimeemurphy9420@protonmail.ca','02253138857',1,'','6a8bf4d5-c068-4f8f-9de2-52ea3eafb1e3',3),('4e5eddf4-3d28-44ce-bd7c-88cdc4bbbb8c',13,'mc_tech','abc123','September Burnett','septemberburnett2263@yahoo.org','03021477255',1,'','2adacd2d-b5c5-4c5d-acd4-5916da3c3250',3),('52928f32-7443-4d09-92e9-d9f69c0b9c50',7,'cuong_itdev','abc123','Ginger Dominguez','gingerdominguez@google.ca','02536287646',1,'','e339a5ca-93cd-4fa6-af04-2144d8e0ec39',3),('6005102f-ea47-4d7a-8718-b6ad64a9c32e',4,'customer5','abc123','James Wilson','jameswilson12@example.com','0956784321',1,'','b78fd79e-7004-4aa5-8c46-c872c5f84039',3),('66de6cf9-15b3-455a-8049-4d52aad27d16',2,'customer3','abc123','Emma Johnson','emmajohnson76@example.com','0909876543',1,'','444bcb07-0ef8-406e-bd22-530ad5acb781',3),('6d5db001-7767-469a-aad6-3249a4254571',16,'cuong_itdev2','abc123','Kennan Stein','kennanstein@protonmail.couk','06511701817',1,'','efd29050-217a-43a3-875e-4184d5872156',3),('725c08ca-9b41-4447-9ebc-65267da75ee3',15,'lemanhcuong','abc123','Brennan Ryan','brennanryan3165@protonmail.com','01261396647',1,'','38c3bc4e-41de-4a9a-b3e5-1ca643637792',3),('816762f5-736d-40e4-9a1d-4d7f2e9ed920',22,'chusan3','abc123','Ross Hanson','rosshanson@hotmail.org','01771815653',1,'','a96edea3-4cba-4f6f-bbf6-6c0be22912f1',2),('9c985bdb-f84e-497a-91af-75ce0022b797',11,'cuong_online','abc123','Mary Fleming','maryfleming1993@google.edu','07736715288',1,'','1279a851-ebda-4b9b-880c-aee2068644c8',3),('a4b539af-9f25-4a3e-95a7-2475b37148c4',12,'michael','abc123','Keaton Oliver','keatonoliver2562@google.ca','04579777239',1,'','1ecbb261-6649-451a-8d39-39ffd6ec19f1',3),('a97a57a9-4a18-4f09-83bc-2199de84cbd6',23,'chusan4','abc123','Joy Dorsey','joydorsey8639@hotmail.couk','08226141632',1,'','c22e68f6-97f6-4775-aa7b-25935707abe9',2),('aa20fe8d-9d88-4eed-925e-05541416f290',6,'ccuong123','abc123','Irene Sherman','irenesherman@outlook.edu','02725625478',1,'','16759957-f579-456c-8680-ddf392ab16d2',3),('c3f96a4d-1686-4667-b296-f2ba7d8f67cf',14,'mc_2024','abc123','Zorita Henry','zoritahenry@google.ca','05046172208',1,'','49868857-65fb-48cc-84a6-1f1a132d4e36',3),('c616da98-3a3b-4726-b7e0-654bddf64275',0,'customer1','abc123','Noah Miller','noahmiller23@example.com','0981234567',1,'','11244830-f73b-4ff2-9fa3-b39aabf5fed2',3),('c61fcbac-2ff0-4716-8bec-0ecb2301e05e',25,'cuonglm','123','Cường','cuonglm@gmail.com','0862200319',1,'','dcb8a686-8b39-4f81-b5ce-dfb4c8f52bc2',2),('c6c9d2bc-98e7-4d15-98d8-2c9945a22a91',21,'chusan2','abc123','Oscar Morrison','oscarmorrison@google.com','08917436957',1,'','42724a1a-b30e-486f-b0cd-6966a2226778',2),('cd10bf5c-b49b-458a-836d-9837e0570018',5,'john_doe','1234','John Doe','johndoe1@example.com','+1234567890',1,'','9be92436-9a7c-45e7-8a87-6b635b6309bf',3),('d7388cb9-c830-41b1-9f27-95aabcb63c36',20,'chusan1','abc123','Valentine Davenport','valentinedavenport@hotmail.com','00158108337',1,'','3d226dbd-1abe-49a4-810c-c7425dd8326a',2),('e0ccdb1c-bd24-48fd-9f63-9d5f912c5c49',8,'cuong_itdev1','abc123','Quin Gonzalez','quingonzalez@protonmail.com','05431641125',1,'','9cbe50f0-748e-4166-85fd-e4287ec18411',3),('f3d57a09-916e-490b-8a69-8856972fe2d7',1,'customer2','abc123','Liam Smith','liamsmith89@example.com','0912345678',1,'','d88f31c1-80a1-4c26-9775-98541b8abb9b',3),('faa8a081-148a-4c53-b767-305d955a3181',10,'leemccc','abc123','Bell Soto','bellsoto@outlook.edu','09513737234',1,'','7aa63e17-44be-4565-85b3-2a7b97f4fb13',3);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-01-04 10:15:17
