DROP DATABASE IF EXISTS randomdice;
CREATE DATABASE randomdice DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

USE randomdice;
CREATE TABLE t_user (
    id VARCHAR(50) NOT NULL, 
    hashedPassword VARCHAR(100) NOT NULL, 
    salt VARCHAR(100) NOT NULL, 
    highestStage INT NOT NULL, 
    highestScore INT NOT NULL,
    PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;