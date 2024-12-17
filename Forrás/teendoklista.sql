-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Dec 17. 10:21
-- Kiszolgáló verziója: 10.4.28-MariaDB
-- PHP verzió: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `teendoklista`
--
CREATE DATABASE IF NOT EXISTS `teendoklista` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `teendoklista`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `feladatok`
--

CREATE TABLE `feladatok` (
  `id` int(11) NOT NULL,
  `cim` varchar(50) NOT NULL,
  `tartalom` text DEFAULT NULL,
  `hatarido` datetime NOT NULL,
  `teljesitve` tinyint(1) NOT NULL DEFAULT 0,
  `felhasznalo_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- A tábla adatainak kiíratása `feladatok`
--

INSERT INTO `feladatok` (`id`, `cim`, `tartalom`, `hatarido`, `teljesitve`, `felhasznalo_id`) VALUES
(1, '1. feladat', 'Tervezés', '2022-09-15 00:00:00', 1, 1),
(2, '2. feladat', 'Megvalósítás', '2022-09-15 00:00:00', 0, 1),
(3, '3. feladat', 'Tesztelés', '2022-12-20 00:00:00', 0, 1),
(4, 'Hosszú', 'Első\rMásodik\rHarmadik\rNegyedik\rÖtödik\rHatodik\rHetedik.', '2024-01-25 01:04:25', 0, 1),
(5, 'felhasználói jegyzet', 'Teszt', '2022-12-17 00:00:00', 1, 2);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasznalok`
--

CREATE TABLE `felhasznalok` (
  `id` int(11) NOT NULL,
  `felhasznalonev` varchar(50) NOT NULL,
  `jelszo` varchar(255) NOT NULL,
  `szerepkor_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- A tábla adatainak kiíratása `felhasznalok`
--

INSERT INTO `felhasznalok` (`id`, `felhasznalonev`, `jelszo`, `szerepkor_id`) VALUES
(1, 'admin', '$2y$04$2roNffFN9MgJEzHplqDdH.Dp4Rma.kheyAQ2IQl53tw/MoKTy1ANy', 1),
(2, 'user', '$2y$04$JJrdWcW4RT6AiICqcJRGzOPa6x7bSoxHAeML7pxaCdnd5Gy8eq/tK', 2);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `login_tokenek`
--

CREATE TABLE `login_tokenek` (
  `id` int(11) NOT NULL,
  `token` varchar(50) NOT NULL,
  `lejarat_datum` datetime NOT NULL,
  `felhasznalo_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szerepkorok`
--

CREATE TABLE `szerepkorok` (
  `id` int(11) NOT NULL,
  `nev` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- A tábla adatainak kiíratása `szerepkorok`
--

INSERT INTO `szerepkorok` (`id`, `nev`) VALUES
(1, 'Adminisztrátor'),
(2, 'Felhasználó');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `feladatok`
--
ALTER TABLE `feladatok`
  ADD PRIMARY KEY (`id`),
  ADD KEY `felhasznalo_id` (`felhasznalo_id`);

--
-- A tábla indexei `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `felhasznalonev` (`felhasznalonev`),
  ADD KEY `szerepkor_id` (`szerepkor_id`);

--
-- A tábla indexei `login_tokenek`
--
ALTER TABLE `login_tokenek`
  ADD PRIMARY KEY (`id`),
  ADD KEY `felhasznalo_id` (`felhasznalo_id`);

--
-- A tábla indexei `szerepkorok`
--
ALTER TABLE `szerepkorok`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `nev` (`nev`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `feladatok`
--
ALTER TABLE `feladatok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT a táblához `felhasznalok`
--
ALTER TABLE `felhasznalok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT a táblához `login_tokenek`
--
ALTER TABLE `login_tokenek`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `szerepkorok`
--
ALTER TABLE `szerepkorok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `feladatok`
--
ALTER TABLE `feladatok`
  ADD CONSTRAINT `feladatok_ibfk_1` FOREIGN KEY (`felhasznalo_id`) REFERENCES `felhasznalok` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD CONSTRAINT `felhasznalok_ibfk_1` FOREIGN KEY (`szerepkor_id`) REFERENCES `szerepkorok` (`id`);

--
-- Megkötések a táblához `login_tokenek`
--
ALTER TABLE `login_tokenek`
  ADD CONSTRAINT `login_tokenek_ibfk_1` FOREIGN KEY (`felhasznalo_id`) REFERENCES `felhasznalok` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
