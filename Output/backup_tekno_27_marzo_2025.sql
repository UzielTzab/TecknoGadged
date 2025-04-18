/*M!999999\- enable the sandbox mode */ 
-- MariaDB dump 10.19  Distrib 10.5.27-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: tecnogadged
-- ------------------------------------------------------
-- Server version	10.5.27-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `customers`
--

DROP TABLE IF EXISTS `customers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `tipo_dispositivo` varchar(50) DEFAULT NULL,
  `marca` varchar(50) DEFAULT NULL,
  `modelo` varchar(50) DEFAULT NULL,
  `estatus` varchar(20) DEFAULT NULL,
  `fecha_entregar` datetime DEFAULT NULL,
  `motivo` varchar(100) DEFAULT NULL,
  `persona_recibio` varchar(50) DEFAULT NULL,
  `fecha_recibido` datetime DEFAULT NULL,
  `persona_reparo` varchar(50) DEFAULT NULL,
  `diagnostico` varchar(500) DEFAULT NULL,
  `fecha_reparado` datetime DEFAULT NULL,
  `costo` int(11) DEFAULT NULL,
  `comentarios` varchar(500) DEFAULT NULL,
  `refaccion` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=137 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customers`
--

LOCK TABLES `customers` WRITE;
/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
INSERT INTO `customers` VALUES (1,'Fredy Chuc','0000000000','Celular','Motorola','Moto E54','ENTREGADO','2025-01-20 18:00:00','Macro/Payjoy','Eduardo','2025-01-19 16:24:38','Eduardo','Se retiro macropay','2025-01-23 20:40:24',700,NULL,NULL),(2,'mofles','0000000000','Celular','Samsung','10','ENTREGADO','2025-01-22 18:00:00','Bateria','Gustavo','2025-01-21 20:36:06','Gustavo','se adapto la bateria correctamente','2025-01-21 20:36:35',40,NULL,NULL),(3,'alejandro pech','0000000000','Laptop','HP','azul','ENTREGADO','2025-01-23 18:00:00','Boton malo','Gustavo','2025-01-22 20:26:49','Eduardo','Se reconstruyo boton de encendido','2025-01-23 22:15:11',200,NULL,NULL),(4,'paloma','0000000000','Celular','Xiaomi','c9','ENTREGADO','2025-01-23 18:00:00','Bateria','Gustavo','2025-01-22 20:27:16','Gustavo','garantia','2025-01-25 13:37:47',0,NULL,NULL),(5,'Alejandro Pech','9992001502','Celular','Huawei','p30 lite','ENTREGADO','2025-01-23 18:00:00','Display roto','Venancio','2025-01-22 20:29:38','Eduardo','Cambio de tarjeta de carga, estaba sulfatado/mojado','2025-02-07 21:43:52',150,NULL,NULL),(6,'Bryan Puch','9996403776','Celular','Samsung','a71','ENTREGADO','2025-01-23 18:00:00','Display roto','Venancio','2025-01-22 20:51:17','Eduardo','Cambio de display','2025-01-23 20:43:42',1000,NULL,NULL),(7,'Diliana Bast','0000000000','Laptop','HP','15-y','ENTREGADO','2025-01-24 18:00:00','No carga SO','Eduardo','2025-01-23 22:19:44','Eduardo','Daño de Tarjeta Main','2025-02-05 10:32:45',0,NULL,NULL),(8,'daniela eb','0000000000','Celular','Motorola','rojo','ENTREGADO','2025-01-26 18:00:00','No carga','Gustavo','2025-01-25 13:21:35','Gustavo','cc','2025-01-25 13:38:06',140,NULL,NULL),(10,'maria','0000000000','Celular','Xiaomi','redmi','ENTREGADO','2025-01-27 18:00:00','No carga','Gustavo','2025-01-26 12:39:08','Gustavo','liberacion, `pedia codigo, ya funciona telcel','2025-01-26 12:41:40',150,NULL,NULL),(11,'xxx','0000000000','Celular','Vivo','y11','ENTREGADO','2025-01-29 18:00:00','Display roto','Gustavo','2025-01-28 20:07:37','Gustavo','cambio de display','2025-01-28 20:10:08',400,NULL,NULL),(17,'Victoria Ciau','0000000000','Celular','Bocina','Stf','ENTREGADO','2025-02-06 18:00:00','No carga','Venancio','2025-02-05 10:31:27','Gustavo','cambio de c,c.','2025-02-08 20:14:15',120,NULL,NULL),(18,'Maria Can','9911080335','Celular','ZTE','Blade A52','ENTREGADO','2025-02-10 18:00:00','Imagen distorcionada, daño en display','Eduardo','2025-02-05 11:02:43','Venancio','Cambio de pantalla','2025-02-08 13:42:34',400,NULL,NULL),(19,'ventura','0000000000','Celular','Huawei','i','REPARADO','2025-02-06 18:00:00','Display roto','Gustavo','2025-02-05 20:55:47','Venancio','Cambio de pantalla y camara trasera','2025-02-09 13:16:30',750,NULL,NULL),(20,'Mini','0000000000','Celular','Oppo','12c','ENTREGADO','2025-02-06 18:00:00','Mojado','Venancio','2025-02-05 21:48:52','Venancio','Daño de tarjeta de carga y pantalla','2025-02-05 21:49:26',600,NULL,NULL),(21,'fatima','0000000000','Celular','Motorola','1','ENTREGADO','2025-02-07 18:00:00','Display roto','Venancio','2025-02-06 20:45:41','Gustavo','cambio de display','2025-02-06 20:50:35',450,NULL,NULL),(22,'juan cituk','0000000000','Celular','Huawei','mate 20 lite','REPARADO','2025-02-07 18:00:00','Boton malo','Venancio','2025-02-06 20:46:41','Gustavo','se reparo boton power','2025-02-06 20:51:05',130,NULL,NULL),(23,'daniel chale','0000000000','Celular','videojuego','x6','ENTREGADO','2025-02-07 18:00:00','no agarra los botones','Gustavo','2025-02-06 20:47:23','Gustavo','mantto','2025-02-06 20:51:30',100,NULL,NULL),(24,'emanuel cauich','0000000000','Celular','ZTE','0','REPARADO','2025-02-07 18:00:00','No carga','Gustavo','2025-02-06 20:47:56','Gustavo','cc y se pego el display','2025-02-06 20:52:03',140,NULL,NULL),(25,'eduardo','0000000000','Bocina','Bose','12','REPARADO','2025-02-07 18:00:00','No carga','Gustavo','2025-02-06 20:48:24','Gustavo','cc','2025-02-06 20:52:18',130,NULL,NULL),(26,'lg','0000000000','Celular','LG','q60','ENTREGADO','2025-02-07 18:00:00','Display roto','Gustavo','2025-02-06 20:48:53','Gustavo','display','2025-02-06 20:52:46',650,NULL,NULL),(27,'Damian May','9992006502','Laptop','Lenovo','xxxx','ENTREGADO','2025-02-08 18:00:00','No prende','Venancio','2025-02-07 19:11:54','Eduardo','Se le puso cargador , se formateo, y se  instalaron apps','2025-02-12 19:37:16',600,NULL,NULL),(28,'Alejandro Pech','9992001502','Celular','Samsung','A12','ENTREGADO','2025-02-08 18:00:00','No carga','Eduardo','2025-02-07 21:43:09','Eduardo','cambio de tarjeta de carga','2025-02-07 21:45:08',150,NULL,NULL),(29,'Brenda Lòpez','0000000000','Celular','Samsung','a13','ENTREGADO','2025-02-09 06:00:00','Display roto','Venancio','2025-02-08 13:43:14','Eduardo','Cambio de display','2025-02-08 20:28:48',400,NULL,NULL),(30,'Saori Alonzo','0000000000','Celular','ZTE','xxx','ENTREGADO','2025-02-09 18:00:00','No carga','Venancio','2025-02-08 13:53:01','Venancio','Cambio de c.c.  y cargador 130+60. ','2025-02-08 13:54:02',190,NULL,NULL),(31,'Lourdes Bastarrachea','0000000000','Celular','Alcatel','xxx','ENTREGADO','2025-02-09 18:00:00','No carga','Venancio','2025-02-08 20:15:45','Gustavo','c c','2025-02-09 11:53:36',140,NULL,NULL),(32,'bon','0000000000','Celular','Samsung','12','ENTREGADO','2025-02-10 18:00:00','Display roto','Gustavo','2025-02-09 11:53:18','Gustavo','c de display','2025-02-09 11:54:17',400,NULL,NULL),(33,'Yuni','0000000000','Celular','Xiaomi','Mi 9 lite','ENTREGADO','2025-02-11 18:00:00','Bateria','Venancio','2025-02-10 08:08:22','Venancio','Bateria inflada/boton de encendido hundido','2025-02-11 19:54:24',380,NULL,NULL),(34,'Jose Luis Uc','0000000000','Celular','Motorola','Moto e6s','ENTREGADO','2025-02-11 18:00:00','No carga','Venancio','2025-02-10 08:09:47','Venancio','Cambio de c.c.','2025-02-10 08:54:38',130,NULL,NULL),(35,'alberto xul canche','0000000000','Celular','Huawei','y9 s','ENTREGADO','2025-02-12 18:00:00','Display roto','Gustavo','2025-02-11 20:24:46','Gustavo','pegar display','2025-02-11 20:25:49',80,NULL,NULL),(36,'alberto xul canche','0000000000','Celular','Huawei','y8','ENTREGADO','2025-02-12 18:00:00','Bateria','Gustavo','2025-02-11 20:25:22','Gustavo','bateria','2025-02-11 21:02:17',300,NULL,NULL),(37,'Sandro Vicinaiz','0000000000','Celular','Samsung','a30','ENTREGADO','2025-02-13 18:00:00','Display roto','Venancio','2025-02-12 19:25:49','Venancio','Cambio de Pantalla','2025-02-15 13:03:19',450,NULL,NULL),(38,'eduardo','0000000000','Tablet','Acer','kuromi','REPARADO','2025-02-13 18:00:00','No carga','Gustavo','2025-02-12 19:57:24','Gustavo','cambio cc','2025-02-12 19:59:18',140,NULL,NULL),(39,'rodolfo','0000000000','Celular','Samsung','01','REPARADO','2025-02-13 18:00:00','Boton malo','Gustavo','2025-02-12 19:58:51','Gustavo','cambio de botones plastico','2025-02-12 20:00:11',100,NULL,NULL),(40,'Fabiola Medina','9992753897','Celular','lenovo','Ideapad 1i','ENTREGADO','2025-02-13 06:00:00','No prende','Venancio','2025-02-12 20:10:37','Venancio','Daño interno de tarjeta main','2025-02-15 13:03:52',0,NULL,NULL),(41,'Isidro Puch','9999999999','Celular','Samsung','Redmi 12 C','ENTREGADO','2025-02-14 18:00:00','Display roto','Eduardo','2025-02-13 19:39:14','Eduardo','Cambio de Pantalla','2025-02-15 13:02:20',400,NULL,NULL),(42,'Damian Cauich','9999999999','Laptop','Toshiba','satellite','ENTREGADO','2025-02-14 18:00:00','Disco duro dañado','Venancio','2025-02-13 20:09:26','Eduardo','Cambio de disco duro e instalacion de sistema y aplicaciones','2025-02-13 20:10:20',600,NULL,NULL),(43,'Michelle Chuc','0000000000','Celular','Samsung','A04','ENTREGADO','2025-02-14 18:00:00','Cta de samsung','Eduardo','2025-02-13 20:16:04','Eduardo','Se elimino cta de samsung','2025-02-13 20:18:26',80,NULL,NULL),(44,'Fredy Chuc','9992700078','Celular','Samsung','a02','NO REPARADO','2025-02-16 06:00:00','Sin señal','Venancio','2025-02-15 12:57:38','Eduardo','Tarjeta principal dañada','2025-02-23 13:41:18',0,NULL,NULL),(45,'Ernesto Alonzo','0000000000','Bocina','Alien Pro','alien pro','NO REPARADO','2025-02-17 18:00:00','No se escucha(Bocina)','Eduardo','2025-02-15 20:24:59','Eduardo','Daño en tarjeta Main','2025-03-07 08:01:34',0,NULL,NULL),(46,'Cecilia Kantun','0000000000','Celular','Motorola','E20','ENTREGADO','2025-02-16 18:00:00','Display roto','Venancio','2025-02-15 20:55:45','Venancio','CAMBIO DE PANTALLA ','2025-02-18 21:21:59',450,NULL,NULL),(47,'Brenda Ayala','0000000000','Celular','Honor','x','ENTREGADO','2025-02-19 18:00:00','Display roto','Eduardo','2025-02-16 11:51:09','Venancio','CAMBIO DE PANTALLA','2025-02-18 21:30:30',400,NULL,NULL),(48,'eduardo chin','0000000000','Celular','Samsung','a30','ENTREGADO','2025-02-17 18:00:00','Display roto','Gustavo','2025-02-16 12:46:03','Gustavo','cambio de display','2025-02-16 12:49:05',450,NULL,NULL),(49,'david rivera','0000000000','Celular','Samsung','j4','ENTREGADO','2025-02-17 18:00:00','Display roto','Gustavo','2025-02-16 12:51:58','Gustavo','c de carga\r\ny cambio de display','2025-02-16 12:53:00',450,NULL,NULL),(50,'Negro Cauich ','0000000000','Bocina','Geartek','x','ENTREGADO','2025-02-17 18:00:00','No carga','Eduardo','2025-02-16 13:47:34','Eduardo','Se adapto cable para cargar','2025-02-17 20:52:47',130,NULL,NULL),(51,'Amilcar','0000000000','Laptop','Sony','VAIO','ENTREGADO','2025-02-18 18:00:00','No carga','Eduardo','2025-02-17 20:54:39','Eduardo','cambio de conector','2025-02-19 21:12:08',200,NULL,NULL),(52,'Rosa Lidia Muñoz','9994156373','Celular','Samsung','a14','EN LABORATORIO','2025-02-18 18:00:00','Macro/Payjoy','Venancio','2025-02-17 09:37:03',NULL,NULL,NULL,NULL,NULL,NULL),(53,'Bryan Puch','9996403776','Celular','Samsung','A71','ENTREGADO','2025-02-18 18:00:00','falla touch','Eduardo','2025-02-17 10:08:20','Gustavo','har rseset y se guardo sus archivos','2025-02-18 20:33:55',150,NULL,NULL),(54,'mili','0000000000','Celular','Samsung','03S','ENTREGADO','2025-02-19 18:00:00','Display roto','Gustavo','2025-02-18 20:59:40','Gustavo','CAMBIO DE DISPLAY(DEJO $200','2025-02-18 21:00:20',400,NULL,NULL),(55,'PATI SOSA','0000000000','Celular','Motorola','60','ENTREGADO','2025-02-19 18:00:00','Display roto','Gustavo','2025-02-18 21:00:56','Eduardo','cambio de display','2025-02-19 21:13:19',500,NULL,NULL),(56,'arturo','0000000000','Celular','Samsung','azul','ENTREGADO','2025-02-20 18:00:00','Boton malo','Gustavo','2025-02-19 19:45:43','Gustavo','cambio del conector','2025-02-19 19:46:16',200,NULL,NULL),(57,'Marco Puc','0000000000','Laptop','HP','xxxx','ENTREGADO','2025-02-20 18:00:00','Activar Officce','Venancio','2025-02-19 21:06:49','Eduardo','Act office','2025-02-20 20:57:42',50,NULL,NULL),(58,'Brena Lopez','0000000000','Tablet','Kidiby ','kids','ENTREGADO','2025-02-20 18:00:00','No carga','Venancio','2025-02-19 21:27:15','Gustavo','daño de logica, la bateria se cargo manual no prendio','2025-02-20 20:51:56',0,NULL,NULL),(59,'Jesus Batastarrachea','0000000000','Celular','Samsung','a03s','ENTREGADO','2025-02-21 18:00:00','Display roto','Venancio','2025-02-20 10:37:52','Venancio','Cambio de display','2025-02-23 13:42:02',400,NULL,NULL),(60,'Venado','0000000000','Bocina','Daewoo','x','REPARADO','2025-02-21 18:00:00','No prende','Eduardo','2025-02-20 19:17:01','Gustavo','se resoldo la entrada de carga y se ajusto el swich de encendido','2025-02-20 20:52:45',130,NULL,NULL),(61,'Oscar Puch','9991410687','Celular','Oppo','Oppo A78','NO REPARADO','2025-02-25 18:00:00','Tiene reporte de robo/extravio','Eduardo','2025-02-21 19:40:05','Eduardo','No hay soporte por el momento de modelo','2025-02-25 20:56:07',0,NULL,NULL),(63,'gabriel gamboa','9911022287','Celular','Apple','iphone 8','ENTREGADO','2025-02-24 18:00:00','No carga','Gustavo','2025-02-23 10:12:18','Venancio','Cambio de bateria','2025-02-25 20:48:12',400,NULL,NULL),(64,'nayeli magadal','0000000000','Celular','Xiaomi','a2','REPARADO','2025-02-24 18:00:00','No carga','Gustavo','2025-02-23 10:42:58','Gustavo','cambio de centro de carga','2025-02-23 10:43:29',130,NULL,NULL),(65,'sdfsf','0000000000','Celular','ZTE','a1','REPARADO','2025-02-24 18:00:00','No carga','Gustavo','2025-02-23 12:31:37','Gustavo','cc','2025-02-23 12:31:59',130,NULL,NULL),(66,'azul','0000000000','Celular','Samsung','j4','REPARADO','2025-02-24 18:00:00','No carga','Gustavo','2025-02-23 12:59:29','Gustavo','cc','2025-02-23 13:00:03',130,NULL,NULL),(67,'Jose Manuel Parra','0000000000','Celular','redmi','13C','ENTREGADO','2025-02-26 18:00:00','Display roto','Eduardo','2025-02-24 19:57:46','Eduardo','Cambio de display','2025-02-26 21:34:46',400,NULL,NULL),(68,'Sergio Eb','0000000000','Celular','Oppo','reno 5 Lite','ENTREGADO','2025-02-26 18:00:00','Display roto','Eduardo','2025-02-24 19:59:51','Gustavo','cambio de display','2025-02-25 20:35:26',400,NULL,NULL),(69,'funda ','0000000000','Celular','Samsung','a02','REPARADO','2025-02-25 18:00:00','Display roto','Gustavo','2025-02-24 21:52:37','Gustavo','pegar display','2025-02-24 21:54:08',80,NULL,NULL),(70,'hilario itza','0000000000','Celular','Motorola','e7i','ENTREGADO','2025-02-26 18:00:00','No prende','Eduardo','2025-02-25 20:03:29','Eduardo','daño en logica','2025-03-05 08:09:58',0,NULL,NULL),(71,'kati','0000000000','Laptop','Samsung','gris','ENTREGADO','2025-02-26 18:00:00','Mantenimiento de parte','Gustavo','2025-02-25 20:33:21','Gustavo','instalar office, activarlo','2025-02-25 20:34:07',80,NULL,NULL),(73,'heidi pareja','0000000000','Celular','Huawei','y9 prime','ENTREGADO','2025-02-27 18:00:00','Display roto','Gustavo','2025-02-26 20:36:21','Gustavo','cambio de display','2025-02-26 20:37:20',400,NULL,NULL),(75,'luis dager','0000000000','Bocina','Kaiser','01','ENTREGADO','2025-02-25 18:00:00','usb','Gustavo','2025-02-26 20:39:25','Gustavo','cambio de la entrada usb, se puenteo ','2025-02-26 20:39:57',200,NULL,NULL),(76,'areixis varguez','0000000000','Celular','tira led','x','ENTREGADO','2025-02-27 18:00:00','pines de tira led desoldado ','Eduardo','2025-02-26 21:32:36','Gustavo','se soldaron los pines','2025-02-27 19:52:07',50,NULL,NULL),(77,'Jose Luis Uc','9993810464','Celular','Motorola','Moto E6i','ENTREGADO','2025-02-27 06:00:00','Mantenimiento de parte','Eduardo','2025-02-26 21:33:45','Eduardo','Cambio de tarjeta de carga','2025-03-05 19:30:24',100,NULL,NULL),(78,'Amilcar','0000000000','Celular','micro','lampara micro','ENTREGADO','2025-02-27 18:00:00','Mantenimiento de parte','Eduardo','2025-02-26 21:37:16','Eduardo','Mantenimiento de parte','2025-02-28 20:41:16',80,NULL,NULL),(79,'David Kuk','9995313509','Celular','Samsung','A03s','ENTREGADO','2025-03-01 18:00:00','Display roto','Eduardo','2025-02-28 19:29:11','Eduardo','Cambio de display, abono 200','2025-03-01 20:09:29',400,NULL,NULL),(80,'Sara Chin','0000000000','Laptop','Lenovo','x','ENTREGADO','2025-03-01 18:00:00','Mantenimiento de parte','Eduardo','2025-02-28 19:50:46','Eduardo','Activacion de office','2025-02-28 20:40:38',80,NULL,NULL),(81,'Nery Miam','0000000000','Celular','Cubot','cubot','ENTREGADO','2025-03-02 18:00:00','No prende','Venancio','2025-03-01 19:37:03','Venancio','Daño de flexor de huella (corto)','2025-03-01 21:00:05',90,NULL,NULL),(82,'Jacobo Cauich','0000000000','Celular','Samsung','Clon','ENTREGADO','2025-03-03 18:00:00','Bateria','Eduardo','2025-03-02 13:54:44','Venancio','bateria','2025-03-05 08:09:29',150,NULL,NULL),(83,'Erick Puc','9994564129','Celular','Motorola','e40','ENTREGADO','2025-03-04 06:00:00','Display roto','Venancio','2025-03-03 21:11:21','Eduardo','display','2025-03-05 08:08:15',400,NULL,NULL),(84,'Alejandro Pech','9995108085','Celular','Samsung','A12','ENTREGADO','2025-03-06 18:00:00','Mantenimiento de parte','Eduardo','2025-03-05 08:13:13','Gustavo','Se resoldo peineta de flexor de display','2025-03-05 08:15:30',100,NULL,NULL),(85,'Bere','9912040123','Celular','Oukitel','C57 Pro','ENTREGADO','2025-03-08 18:00:00','Cta de Google','Eduardo','2025-03-07 07:48:26','Eduardo','Se retiro cuenta','2025-03-07 07:54:42',180,NULL,NULL),(86,'Doña Reina','0000000000','Celular','Ghia','ghia','ENTREGADO','2025-03-08 18:00:00','Bateria','Eduardo','2025-03-07 08:52:05','Gustavo','se adapto bateria','2025-03-07 08:52:58',120,NULL,NULL),(87,'Roger Escobedo','9994475077','Celular','ZTE','v smart','ENTREGADO','2025-03-09 18:00:00','Display roto','Venancio','2025-03-08 11:23:01','Venancio','Cambio de pantalla','2025-03-10 21:57:13',450,NULL,NULL),(88,'Sergio Eb','0000000000','Celular','Oppo','Reno 5 lite','ENTREGADO','2025-03-09 18:00:00','Display roto','Venancio','2025-03-08 12:41:33','Eduardo','Cambio de display','2025-03-10 21:11:27',400,NULL,NULL),(89,'Sergio Eb','0000000000','Celular','Oppo','a15','ENTREGADO','2025-03-09 18:00:00','Display roto','Venancio','2025-03-08 12:43:12','Venancio','Se reparo Display','2025-03-10 21:11:56',250,NULL,NULL),(90,'Mech','0000000000','Celular','Samsung','a50','ENTREGADO','2025-03-09 18:00:00','Display roto','Venancio','2025-03-08 16:34:09','Eduardo','Cambio de display','2025-03-10 21:08:03',500,NULL,NULL),(91,'Lizbeth','9994100975','Celular','Samsung','A55','ENTREGADO','2025-03-09 18:00:00','No carga','Eduardo','2025-03-08 11:18:21','Eduardo','Daño en Main','2025-03-16 11:55:27',0,NULL,NULL),(92,'Danna Marrufo','0000000000','Celular','Samsung','A22','ENTREGADO','2025-03-11 18:00:00','No prende','Venancio','2025-03-10 19:34:24','Eduardo','Cambio de display y tarjeta de carga','2025-03-11 20:52:39',700,NULL,NULL),(93,'Ismael ','9999999999','Celular','ZTE','A71','ENTREGADO','2025-03-11 18:00:00','Display roto','Venancio','2025-03-10 21:21:01','Venancio','Cambio de pantalla','2025-03-11 19:51:58',220,NULL,NULL),(94,'Jorge Carrillo','9997634769','Celular','Oppo','a15','ENTREGADO','2025-03-12 18:00:00','Display roto','Venancio','2025-03-11 12:21:56','Venancio','Cambio de display','2025-03-13 20:06:54',400,NULL,NULL),(95,'Jorge Carrillo','0000000000','Celular','Poco','x3 pro','ENTREGADO','2025-03-12 18:00:00','No prende','Venancio','2025-03-11 13:07:06','Venancio','Mantenimiento de parte','2025-03-13 20:06:15',100,NULL,NULL),(96,'David Mena','0000000000','Celular','Samsung','a03s','ENTREGADO','2025-03-12 18:00:00','Display roto','Venancio','2025-03-11 19:53:30','Gustavo','cambio de display','2025-03-11 20:29:56',450,NULL,NULL),(97,'David Mena','0000000000','Celular','Xiaomi','Redmi 9A','ENTREGADO','2025-03-12 18:00:00','No carga','Venancio','2025-03-11 19:55:27','Gustavo','c de carga','2025-03-11 20:40:31',130,NULL,NULL),(99,'Paty Sosa','9999999999','Celular','Poco','C65','ENTREGADO','2025-03-12 18:00:00','Display roto','Eduardo','2025-03-11 20:50:56','Eduardo','Cambio de display','2025-03-12 19:48:40',450,NULL,NULL),(100,'brenda','0000000000','Tablet','Asus','kibdi','REPARADO','2025-03-13 18:00:00','No carga','Gustavo','2025-03-12 20:32:37','Gustavo','c de carga','2025-03-12 20:33:36',130,NULL,NULL),(101,'eduardo','0000000000','lampara','verde','verde','ENTREGADO','2025-03-13 18:00:00','No carga','Eduardo','2025-03-12 20:33:14','Gustavo','se reparo la carcasa y cambio la bateria','2025-03-12 20:34:16',60,NULL,NULL),(102,'Enrique Puc','9999999999','Celular','Motorola','Moto G20','ENTREGADO','2025-03-14 18:00:00','No carga','Eduardo','2025-03-13 18:55:51','Eduardo','Cambio de Bateria','2025-03-13 19:59:30',300,NULL,NULL),(103,'juan noh','0000000000','Celular','Samsung','j7','ENTREGADO','2025-03-14 18:00:00','Boton malo','Eduardo','2025-03-13 20:14:47','Gustavo','se reparo boton plastico','2025-03-13 20:16:48',100,NULL,NULL),(104,'elvira cauich','0000000000','Celular','Xiaomi','9a','ENTREGADO','2025-03-14 18:00:00','No carga','Eduardo','2025-03-13 20:15:26','Gustavo','cambio de c carga','2025-03-13 20:17:05',130,NULL,NULL),(106,'Ivan Dzul','9999999999','Celular','Samsung','A05','ENTREGADO','2025-03-14 18:00:00','No prende','Eduardo','2025-03-13 21:40:17','Eduardo','Cambio de bateria','2025-03-16 20:32:19',300,NULL,150),(107,'Michelle Puc Puch','9993671843','Celular','Oppo','xxx','REPARADO','2025-03-16 18:00:00','No prende','Venancio','2025-03-15 12:44:05','Gustavo','cambio de display y se reparo boton fisico de power','2025-03-16 12:22:28',450,NULL,NULL),(108,'Isidro puch','9992965645','Celular','Redmi','12c','NO REPARADO','2025-03-16 18:00:00','No carga','Venancio','2025-03-15 20:51:57','Eduardo','Daño en tarjeta principal','2025-03-26 20:24:33',0,'',0),(109,'David Chan','9993614666','Celular','Oppo','Note 11s','NO REPARADO','2025-03-17 18:00:00','No prende','Eduardo','2025-03-16 11:50:57','Eduardo','Daño en tarjeta principal','2025-03-26 20:25:06',0,NULL,0),(110,'enrique','0000000000','Celular','Xiaomi','a2','REPARADO','2025-03-17 18:00:00','Cta de Google','Eduardo','2025-03-16 12:23:12','Gustavo','cuenta de google','2025-03-16 12:24:27',180,NULL,NULL),(114,'Pilon','9999999999','Celular','Motorola','E20','ENTREGADO','2025-03-20 18:00:00','Display roto','Eduardo','2025-03-17 10:17:07','Gustavo','se cambio display','2025-03-18 20:17:36',400,'Sin comentarios',170),(115,'Roberto Cauich','9999999999','Celular','Samsung','Galaxy A04e','ENTREGADO','2025-03-22 18:00:00','Display roto','Eduardo','2025-03-17 10:20:58','Venancio','CAmbio de Pantalla','2025-03-18 21:37:25',450,'Sin comentarios',200),(116,'Sil Kuk','9999999999','Celular','Xiaomi','Redmi 10C','ENTREGADO','2025-03-20 18:00:00','Cta de Google','Eduardo','2025-03-17 10:23:48','Eduardo','Se retiro cta google\r\n','2025-03-19 18:56:09',150,'Sin comentarios',50),(117,'Sergio Eb','9999999999','Celular','Oppo','A17','ENTREGADO','2025-03-19 18:00:00','Bateria','Eduardo','2025-03-17 10:32:47','Venancio','cambio de bateria , pegado de pantalla.','2025-03-18 19:40:59',380,'pantalla despegada, se tiene que pegar',200),(119,'mofles','0000000000','Celular','Samsung','a10','ENTREGADO','2025-03-20 18:00:00','No carga','Gustavo','2025-03-19 20:36:00','Gustavo','cambio de centro de carga y mica','2025-03-19 20:36:27',130,'Sin comentarios',0),(120,'Paty Sosa','9999976778','Celular','Samsung','a04','REPARADO','2025-03-20 18:00:00','Display roto','Venancio','2025-03-19 21:12:48','Eduardo','Cambio de display','2025-03-21 20:10:34',400,'restaurar de fabrica, tiene la info de la cta',200),(121,'tepal','9911113003','Celular','Samsung','a21s','REPARADO','2025-03-20 18:00:00','Display roto','Gustavo','2025-03-19 20:20:25','Eduardo','Cambio de display','2025-03-24 21:03:23',450,'Sin comentarios',0),(122,'Sergio Batarrachea E.','9999999999','Celular','Samsung','Poco M4 Pro','ENTREGADO','2025-03-20 18:00:00','Bateria','Eduardo','2025-03-19 20:43:07','Eduardo','Cambio de bateria','2025-03-19 21:48:06',300,'Bateria inflada',180),(123,'Danisse','9911075696','Celular','Oppo','f3 ','EN LABORATORIO','2025-03-22 18:00:00','Display roto','Venancio','2025-03-21 21:48:43',NULL,NULL,NULL,NULL,'display y tapa rotas',NULL),(124,'Mª Jesus Puch','0000000000','Celular','Samsung','g prime','ENTREGADO','2025-03-22 18:00:00','No carga','Venancio','2025-03-21 23:25:33','Gustavo','se quito la cuenta google, se le creo uno, se le instalo watshap y facebook','2025-03-22 00:45:53',140,'quitar cta de google, poner la que esta en la hoja.',0),(125,'walter','0000000000','Celular','Oppo','a17','ENTREGADO','2025-03-23 18:00:00','Boton malo','Gustavo','2025-03-22 02:17:35','Gustavo','se cambio el bton fisico de power','2025-03-22 02:18:01',150,'Sin comentarios',0),(126,'Rolando Rod','9991150369','celular','redmi','Note 10 pro','EN LABORATORIO','2025-03-25 18:00:00','Display roto','Eduardo','2025-03-24 19:59:30',NULL,NULL,NULL,NULL,'cotizo incell en 450',NULL),(127,'Maria Cen','0000000000','Celular','Xiaomi','Redmi note 8','REPARADO','2025-03-26 18:00:00','Bateria','Eduardo','2025-03-25 20:16:16','Venancio','se cambio la bateria','2025-03-25 20:17:04',280,'cambio de bateria',150),(128,'sergio eb','0000000000','Celular','Oppo','a95','REPARADO','2025-03-26 18:00:00','Display roto','Venancio','2025-03-25 20:27:36','Gustavo','cambio de display','2025-03-25 20:28:02',400,'Sin comentarios',200),(129,'eduardo','0000000000','Celular','Samsung','a14','REPARADO','2025-03-26 18:00:00','Mantenimiento de parte','Eduardo','2025-03-25 20:28:28','Gustavo','se limpio y se pego el display','2025-03-25 20:29:03',80,'pegar display',0),(131,'jose xul','9993670009','Bocina','Kaiser','5004','NO REPARADO','2025-03-26 18:00:00','No prende','Gustavo','2025-03-25 20:45:49','Venancio','Daño en tarjeta','2025-03-27 19:50:12',0,'Sin comentarios',0),(132,'William Bastarrachea','9999999999','Celular','Huawei','Y9 Prime','REPARADO','2025-03-27 18:00:00','No carga','Gustavo','2025-03-26 20:29:30','Gustavo','Cambio de Pin de Carga','2025-03-26 20:30:15',140,'no carga centro de carga',0),(133,'Carlos Bastarrachea','9992694883','Celular','Samsung','A14','EN LABORATORIO','2025-03-28 18:00:00','No prende','Gustavo','2025-03-27 19:16:24',NULL,NULL,NULL,NULL,'Equipo no prende',NULL),(134,'Eduardo Ake Flota','9911052692','Audifono','Panasonic','123','REPARADO','2025-03-28 18:00:00','No carga','Eduardo','2025-03-27 19:17:49','Gustavo','Mantenimiento de parte','2025-03-27 20:48:59',100,'Sin comentarios',0),(135,'Maria Ma. Uicab','5654401367','Celular','Samsung','A35','EN LABORATORIO','2025-03-28 18:00:00','Display roto','Eduardo','2025-03-27 19:18:52',NULL,NULL,NULL,NULL,'Se cayo posible daño de display o falso en conector',NULL),(136,'Carlos Manzanero','9992780354','Celular','ZTE','123','PENDIENTE','2025-03-28 18:00:00','Display roto','Eduardo','2025-03-27 21:08:57',NULL,NULL,NULL,NULL,'Dejo dos equipos mas un Lg y un REdmi 9',NULL);
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `daily_report`
--

DROP TABLE IF EXISTS `daily_report`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `daily_report` (
  `id_reporte_diario` int(11) NOT NULL AUTO_INCREMENT,
  `dia` varchar(50) NOT NULL DEFAULT '',
  `ingreso_generado` decimal(10,2) NOT NULL,
  `costo_refaccion` decimal(10,2) NOT NULL,
  `mano_obra` decimal(10,2) NOT NULL,
  `comision_empleado` decimal(10,2) NOT NULL,
  `fecha_capturada` datetime DEFAULT NULL,
  `id_reporte_semana` int(11) NOT NULL,
  PRIMARY KEY (`id_reporte_diario`),
  KEY `id_reporte_semana` (`id_reporte_semana`),
  CONSTRAINT `daily_report_ibfk_1` FOREIGN KEY (`id_reporte_semana`) REFERENCES `report_per_week` (`id_reporte_semana`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `daily_report`
--

LOCK TABLES `daily_report` WRITE;
/*!40000 ALTER TABLE `daily_report` DISABLE KEYS */;
INSERT INTO `daily_report` VALUES (3,'domingo',300.00,150.00,150.00,60.00,'2025-03-16 20:32:19',2),(4,'martes',380.00,200.00,180.00,72.00,'2025-03-18 19:40:59',3),(5,'martes',400.00,170.00,230.00,92.00,'2025-03-18 20:17:36',4),(6,'martes',450.00,200.00,250.00,100.00,'2025-03-18 21:37:25',3),(7,'miércoles',130.00,0.00,130.00,52.00,'2025-03-19 20:36:27',4),(8,'miércoles',150.00,50.00,100.00,40.00,'2025-03-19 18:56:09',5),(9,'miércoles',300.00,180.00,120.00,48.00,'2025-03-19 21:48:06',5),(10,'viernes',400.00,200.00,200.00,80.00,'2025-03-21 20:10:34',5),(11,'sábado',140.00,0.00,140.00,56.00,'2025-03-22 00:45:53',4),(12,'sábado',150.00,0.00,150.00,60.00,'2025-03-22 02:18:01',4),(13,'lunes',450.00,0.00,450.00,180.00,'2025-03-24 21:03:23',6),(14,'martes',280.00,150.00,130.00,52.00,'2025-03-25 20:17:04',7),(15,'martes',400.00,200.00,200.00,80.00,'2025-03-25 20:28:02',8),(16,'martes',80.00,0.00,80.00,32.00,'2025-03-25 20:29:03',8),(17,'miércoles',0.00,0.00,0.00,0.00,'2025-03-26 20:24:33',6),(18,'miércoles',0.00,0.00,0.00,0.00,'2025-03-26 20:25:06',6),(19,'miércoles',140.00,0.00,140.00,56.00,'2025-03-26 20:30:15',8),(20,'jueves',0.00,0.00,0.00,0.00,'2025-03-27 19:50:12',7),(21,'jueves',100.00,0.00,100.00,40.00,'2025-03-27 20:48:59',8);
/*!40000 ALTER TABLE `daily_report` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employees`
--

DROP TABLE IF EXISTS `employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `employees` (
  `id_empleado` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  PRIMARY KEY (`id_empleado`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employees`
--

LOCK TABLES `employees` WRITE;
/*!40000 ALTER TABLE `employees` DISABLE KEYS */;
INSERT INTO `employees` VALUES (1,'Gustavo'),(2,'Eduardo'),(3,'Venancio');
/*!40000 ALTER TABLE `employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `person`
--

DROP TABLE IF EXISTS `person`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `person` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) DEFAULT NULL,
  `telefono` varchar(50) DEFAULT NULL,
  `salario` decimal(10,2) DEFAULT NULL,
  `lunes` decimal(10,2) DEFAULT NULL,
  `martes` decimal(10,2) DEFAULT NULL,
  `miercoles` decimal(10,2) DEFAULT NULL,
  `jueves` decimal(10,2) DEFAULT NULL,
  `viernes` decimal(10,2) DEFAULT NULL,
  `sabado` decimal(10,2) DEFAULT NULL,
  `domingo` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `person`
--

LOCK TABLES `person` WRITE;
/*!40000 ALTER TABLE `person` DISABLE KEYS */;
INSERT INTO `person` VALUES (1,'Gustavo','',400.00,0.00,0.00,76.00,92.00,0.00,60.00,172.00),(2,'Eduardo','',138.00,0.00,0.00,98.00,40.00,0.00,0.00,0.00),(3,'Venancio','',120.00,0.00,0.00,0.00,120.00,0.00,0.00,0.00);
/*!40000 ALTER TABLE `person` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `report`
--

DROP TABLE IF EXISTS `report`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `report` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `fechaInicio` date DEFAULT NULL,
  `fechaFin` date DEFAULT NULL,
  `ingresoTotal` decimal(10,2) DEFAULT NULL,
  `salarios` decimal(10,2) DEFAULT NULL,
  `manoDeObra` decimal(10,2) DEFAULT NULL,
  `ganancia` decimal(10,2) DEFAULT NULL,
  `refacciones` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `report`
--

LOCK TABLES `report` WRITE;
/*!40000 ALTER TABLE `report` DISABLE KEYS */;
INSERT INTO `report` VALUES (1,'2025-01-20','2025-01-26',2230.00,532.00,1330.00,798.00,900.00),(2,'2025-01-28','2025-02-03',400.00,92.00,230.00,138.00,170.00),(3,'2025-02-05','2025-02-11',5790.00,1416.00,3540.00,2124.00,2250.00),(4,'2025-02-12','2025-02-18',4800.00,1124.00,2810.00,1686.00,1990.00),(5,'2025-02-19','2025-02-25',2980.00,832.00,2080.00,1248.00,900.00),(6,'2025-02-26','2025-03-04',1900.00,524.00,1310.00,786.00,590.00),(7,'2025-03-05','2025-03-11',4150.00,860.00,2150.00,1290.00,2000.00),(8,'2025-03-12','2025-03-18',2450.00,658.00,1645.00,987.00,805.00);
/*!40000 ALTER TABLE `report` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `report_per_week`
--

DROP TABLE IF EXISTS `report_per_week`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `report_per_week` (
  `id_reporte_semana` int(11) NOT NULL AUTO_INCREMENT,
  `fecha_inicio` date NOT NULL,
  `fecha_final` date NOT NULL,
  `id_empleado` int(11) NOT NULL,
  PRIMARY KEY (`id_reporte_semana`),
  KEY `id_empleado` (`id_empleado`),
  CONSTRAINT `report_per_week_ibfk_1` FOREIGN KEY (`id_empleado`) REFERENCES `employees` (`id_empleado`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `report_per_week`
--

LOCK TABLES `report_per_week` WRITE;
/*!40000 ALTER TABLE `report_per_week` DISABLE KEYS */;
INSERT INTO `report_per_week` VALUES (2,'2025-03-10','2025-03-16',2),(3,'2025-03-17','2025-03-23',3),(4,'2025-03-17','2025-03-23',1),(5,'2025-03-17','2025-03-23',2),(6,'2025-03-24','2025-03-30',2),(7,'2025-03-24','2025-03-30',3),(8,'2025-03-24','2025-03-30',1);
/*!40000 ALTER TABLE `report_per_week` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-03-27 21:32:31
