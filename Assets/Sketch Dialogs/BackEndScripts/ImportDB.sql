
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dialogsdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `Character_Values`
--

CREATE TABLE `Character_Values` (
  `ID` int(10) NOT NULL,
  `ValueID` tinyint(3) NOT NULL,
  `CharacterID` int(10) NOT NULL,
  `Amount` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Character_Values`
--

INSERT INTO `Character_Values` (`ID`, `ValueID`, `CharacterID`, `Amount`) VALUES
(1, 1, 0, 20),
(2, 2, 0, 10),
(3, 3, 0, 10);

-- --------------------------------------------------------

--
-- Table structure for table `Conditions_Scenario`
--

CREATE TABLE `Conditions_Scenario` (
  `ID` int(15) NOT NULL,
  `TypeOfLink` int(1) NOT NULL,
  `LinkID` int(10) NOT NULL,
  `TypeOfCondition` tinyint(1) NOT NULL DEFAULT '1',
  `StatToCheck` varchar(50) NOT NULL DEFAULT '',
  `IDCheck` int(10) NOT NULL DEFAULT '0',
  `Min` tinyint(3) NOT NULL DEFAULT '1',
  `Max` tinyint(3) NOT NULL DEFAULT '100'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Conditions_Scenario`
--

INSERT INTO `Conditions_Scenario` (`ID`, `TypeOfLink`, `LinkID`, `TypeOfCondition`, `StatToCheck`, `IDCheck`, `Min`, `Max`) VALUES
(1, 3, 4, 2, 'Replica', 2, 0, 0),
(2, 3, 5, 2, 'Replica', 3, 0, 0),
(3, 2, 4, 2, 'Replica', 5, 0, 0),
(4, 2, 5, 2, 'Replica', 4, 0, 0),
(5, 3, 10, 1, 'Strength', 1, 10, 0),
(6, 3, 11, 2, 'Replica', 8, 0, 0),
(7, 3, 12, 2, 'Replica', 9, 0, 0),
(8, 3, 13, 2, 'Replica', 10, 0, 0),
(9, 2, 13, 2, 'Replica', 13, 0, 0),
(10, 4, 15, 3, 'Strength', 1, 1, 0),
(11, 4, 15, 3, 'Stamina', 3, 1, 0),
(12, 4, 16, 3, 'Intellect', 4, 2, 0),
(13, 4, 17, 3, 'Charisma', 5, 2, 0),
(14, 4, 19, 3, 'Charisma', 5, 2, 0),
(15, 4, 20, 3, 'Intellect', 4, 3, 0),
(16, 4, 21, 3, 'Stamina', 3, 2, 0),
(17, 4, 23, 3, 'Agility', 2, 3, 0),
(18, 4, 24, 3, 'Strength', 1, 2, 0),
(19, 4, 25, 3, 'Intellect', 4, 2, 0);

-- --------------------------------------------------------

--
-- Table structure for table `Info_Scenario`
--

CREATE TABLE `Info_Scenario` (
  `ID` int(10) NOT NULL,
  `Name` varchar(2000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Info_Scenario`
--

INSERT INTO `Info_Scenario` (`ID`, `Name`) VALUES
(1, 'Demo Scenario');

-- --------------------------------------------------------

--
-- Table structure for table `Info_Scenario_Characters`
--

CREATE TABLE `Info_Scenario_Characters` (
  `ID` int(10) NOT NULL,
  `ScenarioID` int(10) NOT NULL,
  `Place` tinyint(1) NOT NULL,
  `Name` varchar(2000) NOT NULL,
  `ImageID` varchar(300) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Info_Scenario_Characters`
--

INSERT INTO `Info_Scenario_Characters` (`ID`, `ScenarioID`, `Place`, `Name`, `ImageID`) VALUES
(0, 1, 0, 'John Doe', '1'),
(1, 1, 1, 'Adam Smith', '2'),
(2, 1, 2, 'Jane Smith', '3'),
(3, 1, 3, 'Luke Smith', '4');

-- --------------------------------------------------------

--
-- Table structure for table `Info_Scenario_Replica`
--

CREATE TABLE `Info_Scenario_Replica` (
  `ID` int(10) NOT NULL,
  `StageID` int(10) NOT NULL,
  `ReplicaType` varchar(50) NOT NULL,
  `Text` varchar(8000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Info_Scenario_Replica`
--

INSERT INTO `Info_Scenario_Replica` (`ID`, `StageID`, `ReplicaType`, `Text`) VALUES
(1, 1, 'Speach', 'Hello, adventurer!\\n\\nDo you need a demonstration of the dialogue system?'),
(2, 2, 'Story', 'Yes!'),
(3, 2, 'Story', 'No!'),
(4, 3, 'Action', 'Okay, then let\'s get started!'),
(5, 3, 'Thoughts', 'Okay, then see you soon! Bye!'),
(6, 6, 'Speach', 'Good choice, [UserName]!\\n\\nMy name is Jane Smith. Very nice to meet you!'),
(7, 9, 'Shout', 'Very nice! My name is Adam!\\n\\nPlease take a short survey to test your abilities.'),
(8, 10, 'Story', 'Okay, I hope this is fast enough...'),
(9, 10, 'Story', 'Hmm... why do I need this?'),
(10, 10, 'Story', 'No! I don\'t want to waste my time'),
(11, 11, 'Thoughts', 'Of course... it won\'t take long. Maybe...'),
(12, 12, 'Story', 'We need to check your psychological profile to understand who we are dealing with.'),
(13, 12, 'Shout', 'Okay, then we\'re done with you. Bye!'),
(14, 14, 'Story', 'Looking through your family tree and photos of your ancestors, you unwittingly note that all your ancestors...'),
(15, 14, 'Story', '...enjoyed exceptional health and lived a long life.'),
(16, 14, 'Story', '...were amazingly smart people, many of the achievements of world science and culture appeared thanks to their talent.'),
(17, 14, 'Story', '...successfully led tens of thousands of people, some of them have written their names in history.'),
(18, 15, 'Story', 'What is Love for you?'),
(19, 15, 'Story', 'The most beautiful and unique feeling on earth. He who did not love did not live.'),
(20, 15, 'Story', 'A complex chemical cocktail of hormones, pheromones and neural impulses in the brain.'),
(21, 15, 'Story', 'The primary instinct responsible for the formation of healthy offspring.'),
(22, 16, 'Story', 'You are attending a masquerade ball at the invitation of your old friend. Everything around is replete with beautiful dresses, exquisite jewelry and elaborate masks. There is a spirit of mystery, passion and vice in the air... But how do you feel in this environment?'),
(23, 16, 'Story', 'Like a fish in water. You literally dissolve into the exuberant magic of what is happening.'),
(24, 16, 'Story', 'Are you bored. You want to take off your stupid mask and go to your cozy laboratory.'),
(25, 16, 'Story', 'You feel uncomfortable among these people, but you overpower yourself to find valuable acquaintances.'),
(26, 18, 'Action', 'Very nice! We are done for now! Bye!'),
(27, 19, 'Shout', 'Thank you all!');

-- --------------------------------------------------------

--
-- Table structure for table `Info_Scenario_Stage`
--

CREATE TABLE `Info_Scenario_Stage` (
  `ID` int(10) NOT NULL,
  `StageOrder` smallint(5) NOT NULL,
  `ScenarioID` int(10) NOT NULL,
  `PersonID` int(10) NOT NULL,
  `StageType` varchar(20) NOT NULL,
  `Special` tinyint(1) NOT NULL,
  `Delay` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Info_Scenario_Stage`
--

INSERT INTO `Info_Scenario_Stage` (`ID`, `StageOrder`, `ScenarioID`, `PersonID`, `StageType`, `Special`, `Delay`) VALUES
(1, 1, 1, 1, 'Talk', 0, 1),
(2, 2, 1, 0, 'Talk', 0, 1),
(3, 3, 1, 2, 'Talk', 0, 1),
(4, 4, 1, 0, 'Finish', 1, 1),
(5, 5, 1, 0, 'Player_Name', 1, 1),
(6, 6, 1, 3, 'Talk', 0, 1),
(7, 7, 1, 0, 'Player_Avatar', 1, 1),
(8, 8, 1, 0, 'Player_Gender', 1, 1),
(9, 9, 1, 1, 'Talk', 0, 1),
(10, 10, 1, 0, 'Talk', 0, 1),
(11, 11, 1, 2, 'Talk', 0, 1),
(12, 12, 1, 3, 'Talk', 0, 1),
(13, 13, 1, 0, 'Finish', 1, 1),
(14, 14, 1, 0, 'Quiz', 1, 1),
(15, 15, 1, 0, 'Quiz', 1, 1),
(16, 16, 1, 0, 'Quiz', 1, 1),
(17, 17, 1, 0, 'Resolve_Quiz', 1, 1),
(18, 18, 1, 3, 'Talk', 0, 1),
(19, 19, 1, 0, 'Talk', 0, 1),
(20, 20, 1, 0, 'Finish', 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `Info_Values`
--

CREATE TABLE `Info_Values` (
  `ValueID` tinyint(3) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `ImageID` varchar(300) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Info_Values`
--

INSERT INTO `Info_Values` (`ValueID`, `Name`, `ImageID`) VALUES
(1, 'Strength', 'C_Strength'),
(2, 'Agility', 'C_Agility'),
(3, 'Stamina', 'C_Stamina'),
(4, 'Intellect', 'C_Intellect'),
(5, 'Charisma', 'C_Charisma');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Character_Values`
--
ALTER TABLE `Character_Values`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Char_ValueID` (`ValueID`,`CharacterID`);

--
-- Indexes for table `Conditions_Scenario`
--
ALTER TABLE `Conditions_Scenario`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `ActivityID` (`LinkID`);

--
-- Indexes for table `Info_Scenario`
--
ALTER TABLE `Info_Scenario`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `Info_Scenario_Characters`
--
ALTER TABLE `Info_Scenario_Characters`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `ScenarioID` (`ScenarioID`);

--
-- Indexes for table `Info_Scenario_Replica`
--
ALTER TABLE `Info_Scenario_Replica`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `StageID` (`StageID`);

--
-- Indexes for table `Info_Scenario_Stage`
--
ALTER TABLE `Info_Scenario_Stage`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `ScenarioID` (`ScenarioID`);

--
-- Indexes for table `Info_Values`
--
ALTER TABLE `Info_Values`
  ADD PRIMARY KEY (`ValueID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Character_Values`
--
ALTER TABLE `Character_Values`
  MODIFY `ID` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `Conditions_Scenario`
--
ALTER TABLE `Conditions_Scenario`
  MODIFY `ID` int(15) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;
--
-- AUTO_INCREMENT for table `Info_Scenario`
--
ALTER TABLE `Info_Scenario`
  MODIFY `ID` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `Info_Scenario_Characters`
--
ALTER TABLE `Info_Scenario_Characters`
  MODIFY `ID` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `Info_Scenario_Replica`
--
ALTER TABLE `Info_Scenario_Replica`
  MODIFY `ID` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;
--
-- AUTO_INCREMENT for table `Info_Scenario_Stage`
--
ALTER TABLE `Info_Scenario_Stage`
  MODIFY `ID` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
--
-- AUTO_INCREMENT for table `Info_Values`
--
ALTER TABLE `Info_Values`
  MODIFY `ValueID` tinyint(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
