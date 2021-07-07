-- phpMyAdmin SQL Dump
-- version 4.9.5
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Jul 07, 2021 at 03:41 PM
-- Server version: 5.7.24
-- PHP Version: 7.4.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `unityaccess`
--

-- --------------------------------------------------------

--
-- Table structure for table `body_parts`
--

CREATE TABLE `body_parts` (
  `body_part_id` int(10) NOT NULL,
  `body_part_name` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `body_parts`
--

INSERT INTO `body_parts` (`body_part_id`, `body_part_name`) VALUES
(1, 'head'),
(2, 'body'),
(3, 'leg');

-- --------------------------------------------------------

--
-- Table structure for table `boosters`
--

CREATE TABLE `boosters` (
  `booster_id` int(10) NOT NULL,
  `booster_name` varchar(35) NOT NULL,
  `booster_features` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `boosters`
--

INSERT INTO `boosters` (`booster_id`, `booster_name`, `booster_features`) VALUES
(1, 'Усилитель здоровья. Ур.1', 25),
(2, 'Усилитель здоровья. Ур.2', 50),
(3, 'Усилитель здоровья. Ур. 3', 80);

-- --------------------------------------------------------

--
-- Table structure for table `bows`
--

CREATE TABLE `bows` (
  `bow_id` int(10) NOT NULL,
  `bow_name` varchar(25) NOT NULL,
  `bow_damage` int(10) NOT NULL,
  `arrows` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bows`
--

INSERT INTO `bows` (`bow_id`, `bow_name`, `bow_damage`, `arrows`) VALUES
(1, 'Стандартный лук', 25, 60),
(2, 'Улучшенный лук', 35, 100);

-- --------------------------------------------------------

--
-- Table structure for table `characters`
--

CREATE TABLE `characters` (
  `character_id` int(10) NOT NULL,
  `score` int(10) NOT NULL,
  `head` int(10) NOT NULL,
  `body` int(10) NOT NULL,
  `legs` int(10) NOT NULL,
  `sword` int(10) NOT NULL,
  `bow` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `characters`
--

INSERT INTO `characters` (`character_id`, `score`, `head`, `body`, `legs`, `sword`, `bow`) VALUES
(1, 0, 1, 2, 3, 1, 1),
(2, 40, 4, 5, 6, 1, 1),
(3, 80, 1, 2, 3, 2, 2);

-- --------------------------------------------------------

--
-- Table structure for table `character_booster`
--

CREATE TABLE `character_booster` (
  `character_id` int(10) NOT NULL,
  `booster_id` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `character_booster`
--

INSERT INTO `character_booster` (`character_id`, `booster_id`) VALUES
(2, 1),
(2, 1),
(3, 1);

-- --------------------------------------------------------

--
-- Table structure for table `clans`
--

CREATE TABLE `clans` (
  `clan_id` int(10) NOT NULL,
  `clan_name` varchar(25) NOT NULL,
  `clan_raiting` int(10) NOT NULL DEFAULT '0',
  `clan_leader` int(35) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `clans`
--

INSERT INTO `clans` (`clan_id`, `clan_name`, `clan_raiting`, `clan_leader`) VALUES
(24, 'superclan', 0, 1),
(25, 'qwrwafvaw', 80, 3);

-- --------------------------------------------------------

--
-- Table structure for table `equipments`
--

CREATE TABLE `equipments` (
  `item_id` int(11) NOT NULL,
  `body_part` int(10) NOT NULL,
  `item_name` varchar(35) NOT NULL,
  `armor` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `equipments`
--

INSERT INTO `equipments` (`item_id`, `body_part`, `item_name`, `armor`) VALUES
(1, 1, 'Стальной шлем', 5),
(2, 2, 'Стальная броня', 10),
(3, 3, 'Стальные штаны', 5),
(4, 1, 'Самурайский шлем', 8),
(5, 2, 'Латный нагрудник', 15),
(6, 3, 'Латные штаны', 8);

-- --------------------------------------------------------

--
-- Table structure for table `players`
--

CREATE TABLE `players` (
  `id` int(10) NOT NULL,
  `username` varchar(16) NOT NULL,
  `hash` varchar(100) NOT NULL,
  `salt` varchar(50) NOT NULL,
  `email` varchar(35) NOT NULL,
  `character_id` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `players`
--

INSERT INTO `players` (`id`, `username`, `hash`, `salt`, `email`, `character_id`) VALUES
(1, 'qwerty123', '$5$rounds=5000$steamedhamsqwert$6C7d69qkZMDFNrmPvEaXnOAZuIyw/TLFuOMBPReyzj7', '$5$rounds=5000$steamedhamsqwerty123$', 'qwreqwr@gmail.com', 1),
(2, 'test12345', '$5$rounds=5000$steamedhamstest1$rPzjR/8s3ZieHkNTZ3U1znJt7X5nbP27CJH1Gfpv2yD', '$5$rounds=5000$steamedhamstest12345$', 'wqfw@gmail.com', 2),
(3, 'qwertyu123', '$5$rounds=5000$steamedhamsqwert$6C7d69qkZMDFNrmPvEaXnOAZuIyw/TLFuOMBPReyzj7', '$5$rounds=5000$steamedhamsqwertyu123$', 'nvpopov.me@gmail.com', 3);

-- --------------------------------------------------------

--
-- Table structure for table `player_clans`
--

CREATE TABLE `player_clans` (
  `player_id` int(10) NOT NULL,
  `clan_id` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `player_clans`
--

INSERT INTO `player_clans` (`player_id`, `clan_id`) VALUES
(1, 24),
(3, 25);

-- --------------------------------------------------------

--
-- Table structure for table `swords`
--

CREATE TABLE `swords` (
  `sword_id` int(10) NOT NULL,
  `sword_name` varchar(25) NOT NULL,
  `sword_damage` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `swords`
--

INSERT INTO `swords` (`sword_id`, `sword_name`, `sword_damage`) VALUES
(1, 'Стальной меч', 15),
(2, 'Стальной топор', 20);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `body_parts`
--
ALTER TABLE `body_parts`
  ADD PRIMARY KEY (`body_part_id`);

--
-- Indexes for table `boosters`
--
ALTER TABLE `boosters`
  ADD PRIMARY KEY (`booster_id`);

--
-- Indexes for table `bows`
--
ALTER TABLE `bows`
  ADD PRIMARY KEY (`bow_id`);

--
-- Indexes for table `characters`
--
ALTER TABLE `characters`
  ADD PRIMARY KEY (`character_id`),
  ADD KEY `head` (`head`,`body`,`legs`,`sword`,`bow`),
  ADD KEY `body` (`body`),
  ADD KEY `legs` (`legs`),
  ADD KEY `bow` (`bow`),
  ADD KEY `sword` (`sword`);

--
-- Indexes for table `character_booster`
--
ALTER TABLE `character_booster`
  ADD KEY `character_id` (`character_id`),
  ADD KEY `booster_id` (`booster_id`);

--
-- Indexes for table `clans`
--
ALTER TABLE `clans`
  ADD PRIMARY KEY (`clan_id`),
  ADD KEY `clan_leader` (`clan_leader`);

--
-- Indexes for table `equipments`
--
ALTER TABLE `equipments`
  ADD PRIMARY KEY (`item_id`),
  ADD KEY `body_part` (`body_part`);

--
-- Indexes for table `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`),
  ADD UNIQUE KEY `character_id` (`character_id`);

--
-- Indexes for table `player_clans`
--
ALTER TABLE `player_clans`
  ADD UNIQUE KEY `player_id` (`player_id`),
  ADD KEY `clan_id` (`clan_id`);

--
-- Indexes for table `swords`
--
ALTER TABLE `swords`
  ADD PRIMARY KEY (`sword_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `body_parts`
--
ALTER TABLE `body_parts`
  MODIFY `body_part_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `boosters`
--
ALTER TABLE `boosters`
  MODIFY `booster_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `bows`
--
ALTER TABLE `bows`
  MODIFY `bow_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `characters`
--
ALTER TABLE `characters`
  MODIFY `character_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `clans`
--
ALTER TABLE `clans`
  MODIFY `clan_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `equipments`
--
ALTER TABLE `equipments`
  MODIFY `item_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `players`
--
ALTER TABLE `players`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `swords`
--
ALTER TABLE `swords`
  MODIFY `sword_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `characters`
--
ALTER TABLE `characters`
  ADD CONSTRAINT `characters_ibfk_1` FOREIGN KEY (`head`) REFERENCES `equipments` (`item_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `characters_ibfk_2` FOREIGN KEY (`body`) REFERENCES `equipments` (`item_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `characters_ibfk_3` FOREIGN KEY (`legs`) REFERENCES `equipments` (`item_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `characters_ibfk_4` FOREIGN KEY (`bow`) REFERENCES `bows` (`bow_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `characters_ibfk_5` FOREIGN KEY (`sword`) REFERENCES `swords` (`sword_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `character_booster`
--
ALTER TABLE `character_booster`
  ADD CONSTRAINT `character_booster_ibfk_1` FOREIGN KEY (`booster_id`) REFERENCES `boosters` (`booster_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `character_booster_ibfk_2` FOREIGN KEY (`character_id`) REFERENCES `characters` (`character_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `equipments`
--
ALTER TABLE `equipments`
  ADD CONSTRAINT `equipments_ibfk_1` FOREIGN KEY (`body_part`) REFERENCES `body_parts` (`body_part_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `players`
--
ALTER TABLE `players`
  ADD CONSTRAINT `players_ibfk_1` FOREIGN KEY (`character_id`) REFERENCES `characters` (`character_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `player_clans`
--
ALTER TABLE `player_clans`
  ADD CONSTRAINT `player_clans_ibfk_1` FOREIGN KEY (`player_id`) REFERENCES `players` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `player_clans_ibfk_2` FOREIGN KEY (`clan_id`) REFERENCES `clans` (`clan_id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
