DROP DATABASE twitchBot;
CREATE DATABASE twitchBot;
USE twitchBot;
CREATE TABLE regex_options (
	ID int NOT NULL AUTO_INCREMENT,
	Name varchar(255),
	Expression varchar(255),
	TargetMethod varchar(255),
	PRIMARY KEY (ID)
);


