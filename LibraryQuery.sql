CREATE DATABASE RoxoftLibrary;

USE RoxoftLibrary;

CREATE TABLE Author 
(
	AuthorId	INT
				PRIMARY KEY
				IDENTITY(1,1),
	FirstName	NVARCHAR(40) NOT NULL,
	LastName	NVARCHAR(40) NOT NULL,
	ShortBio	NVARCHAR(255) NULL
);

CREATE TABLE PlaceOfResidence
(
	PlaceOfResidenceId	INT
						PRIMARY KEY
						IDENTITY(1,1),
	Name				NVARCHAR(40) NOT NULL,
	PostalCode			INT NOT NULL
);

CREATE TABLE Book
(
	BookId		INT
				PRIMARY KEY
				IDENTITY(1,1),
	Title		NVARCHAR(100) NOT NULL,
	Genre		NVARCHAR(40) NULL,
	Author		INT FOREIGN KEY REFERENCES Author (AuthorId)
);

CREATE TABLE Member
(
	MemberId			INT
						PRIMARY KEY
						IDENTITY(1,1),
	FirstName			NVARCHAR(40) NOT NULL,
	LastName			NVARCHAR(40) NOT NULL,
	DateOfBirth			DATE NULL,
	Contact				NVARCHAR(50) NOT NULL,
	Address				NVARCHAR(50) NOT NULL,
	PlaceOfResidence	INT FOREIGN KEY REFERENCES PlaceOfResidence (PlaceOfResidenceId)
);

CREATE TABLE BorrowedBook
(
	BorrowedBookId		INT
						PRIMARY KEY
						IDENTITY(1,1),
	Book				INT FOREIGN KEY REFERENCES Book (BookId),
	Member				INT FOREIGN KEY REFERENCES Member (MemberId),
	CheckoutDate		DATE NOT NULL,
	ReturnDate			DATE NULL
);

INSERT INTO Author VALUES 
	('J.K.', 'Rowling', 'Author of the Harry Potter series'),
	('George', 'Orwell', 'Author of 1984 and Animal Farm'),
	('Stephen', 'King', 'Bestselling author known for horror and suspense novels'),
	('Jane', 'Austen', 'Renowned for novels like Pride and Prejudice and Sense and Sensibility'),
	('Harper', 'Lee', 'Author of To Kill a Mockingbird');

INSERT INTO PlaceOfResidence VALUES 
	('Valpovo', 31550),
	('Zagreb', 10000),
	('Varazdin', 42000),
	('Osijek', 31000),
	('Virovitica', 33000);

INSERT INTO Book VALUES 
	('Harry Potter and the Philosopher''s Stone', 'Fantasy', 1),
	('1984', 'Dystopian fiction', 2),
	('The Shining', 'Horror', 3),
	('Pride and Prejudice', 'Romance', 4),
	('To Kill a Mockingbird', 'Southern Gothic', 5);

INSERT INTO Member VALUES 
	('Michael', 'Scott', '1964-03-15', 'michael.scott@dundermifflin.com', '1725 Condo', 1),
	('Jim', 'Halpert', '1978-10-01', 'jim.halpert@dundermifflin.com', '5351 Chill Avenue', 2),
	('Pam', 'Beesly', '1979-03-25', 'pam.beesly@dundermifflin.com', '12345 Art Street', 3),
	('Dwight', 'Schrute', '1970-01-20', 'dwight.schrute@dundermifflin.com', '42 Beet Street', 4),
	('Angela', 'Martin', '1971-06-25', 'angela.martin@dundermifflin.com', '193 Cat Square', 5);

INSERT INTO BorrowedBook VALUES 
	(1, 1, '2024-05-01', NULL),
	(2, 2, '2024-04-20', '2024-05-10'),
	(3, 3, '2024-05-05', NULL),
	(4, 4, '2024-05-01', '2024-05-12'),
	(5, 5, '2024-05-10', NULL);

