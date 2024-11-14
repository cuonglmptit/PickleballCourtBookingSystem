use `freedb_PickleBallCourtBookingSystem`;

CREATE TABLE Address (
    id CHAR(36) PRIMARY KEY,
    city VARCHAR(50),
    district VARCHAR(50),
    ward VARCHAR(50),
    street VARCHAR(50)
);

CREATE TABLE User (
    id CHAR(36) PRIMARY KEY,
    code INT UNIQUE,
    username VARCHAR(255) UNIQUE,
    password VARCHAR(255),
    name VARCHAR(255),
    email VARCHAR(255),
    phoneNumber VARCHAR(25),
    isActive INT,
    avatarUrl VARCHAR(255),
    addressId CHAR(36),
    FOREIGN KEY (addressId) REFERENCES Address(id)
);

CREATE TABLE Role (
    id CHAR(36) PRIMARY KEY,
    roleName VARCHAR(15)
);

CREATE TABLE UserRole (
    id CHAR(36) PRIMARY KEY,
    userId CHAR(36),
    roleId CHAR(36),
    FOREIGN KEY (userId) REFERENCES User(id),
    FOREIGN KEY (roleId) REFERENCES Role(id)
);

CREATE TABLE CourtOwner (
    id CHAR(36) PRIMARY KEY,
    userId CHAR(36),
    FOREIGN KEY (userId) REFERENCES User(id)
);

CREATE TABLE CourtCluster (
    id CHAR(36) PRIMARY KEY,
    name VARCHAR(255),
    openingTime TIME,
    closeingTime TIME,
    description VARCHAR(255),
    addressId CHAR(36),
    courtOwnerId CHAR(36),
    FOREIGN KEY (addressId) REFERENCES Address(id),
    FOREIGN KEY (courtOwnerId) REFERENCES CourtOwner(id)
);

CREATE TABLE CourtPrice (
    id CHAR(36) PRIMARY KEY,
    time TIME,
    price DOUBLE,
    courtClusterId CHAR(36),
    FOREIGN KEY (courtClusterId) REFERENCES CourtCluster(id)
);

CREATE TABLE Court (
    id CHAR(36) PRIMARY KEY,
    courtNumber INT,
    description VARCHAR(255),
    courtClusterId CHAR(36),
    FOREIGN KEY (courtClusterId) REFERENCES CourtCluster(id)
);

CREATE TABLE ImageCourtURL (
    id CHAR(36) PRIMARY KEY,
    url VARCHAR(255),
    courtClusterId CHAR(36),
    FOREIGN KEY (courtClusterId) REFERENCES CourtCluster(id)
);

CREATE TABLE CourtTimeSlot (
    id CHAR(36) PRIMARY KEY,
    date DATE,
    time TIME,
    isAvailable INT,
    price DOUBLE,
    courtId CHAR(36),
    FOREIGN KEY (courtId) REFERENCES Court(id)
);

CREATE TABLE Customer (
    id CHAR(36) PRIMARY KEY,
    userId CHAR(36),
    FOREIGN KEY (userId) REFERENCES User(id)
);

CREATE TABLE Admin (
    id CHAR(36) PRIMARY KEY,
    userId CHAR(36),
    FOREIGN KEY (userId) REFERENCES User(id)
);

CREATE TABLE Booking (
    id CHAR(36) PRIMARY KEY,
    timeBooking TIMESTAMP,
    amount DOUBLE,
    paymentStatus INT,
    courtId CHAR(36),
    customerId CHAR(36),
    FOREIGN KEY (courtId) REFERENCES Court(id),
    FOREIGN KEY (customerId) REFERENCES Customer(id)
);

CREATE TABLE CourtTimeBooking (
	id CHAR(36) PRIMARY KEY,
    bookingId CHAR(36),
    courtTimeSlotId CHAR(36),
    FOREIGN KEY (bookingId) REFERENCES Booking(id),
    FOREIGN KEY (courtTimeSlotId) REFERENCES CourtTimeSlot(id)
);

CREATE TABLE Feedback (
    id CHAR(36) PRIMARY KEY,
    rating FLOAT(10),
    comment VARCHAR(100),
    courtId CHAR(36),
    bookingId CHAR(36),
    customerId CHAR(36),
    FOREIGN KEY (bookingId) REFERENCES Booking(id),
    FOREIGN KEY (courtId) REFERENCES Court(id),
    FOREIGN KEY (customerId) REFERENCES Customer(id)
);

CREATE TABLE Cancellation (
    id CHAR(36) PRIMARY KEY,
    timeCancel DATETIME,
    bookingId CHAR(36),
    FOREIGN KEY (bookingId) REFERENCES Booking(id)
);
