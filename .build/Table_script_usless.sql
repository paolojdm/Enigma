
DROP INDEX IX_Carts_InventoryID ON Carts;

CREATE TABLE Users (
    Id INT PRIMARY KEY,
    UserName NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    ShippingAddress NVARCHAR(255),
    CreatedAt DATETIME NOT NULL
);

CREATE TABLE Products (
    Id INT PRIMARY KEY,
    ProductName NVARCHAR(255) NOT NULL,
    ProductDescription NVARCHAR(MAX),
    Price FLOAT NOT NULL,
    ProductType NVARCHAR(255) NOT NULL
);

CREATE TABLE Inventory (
    Id INT PRIMARY KEY,
    ProductID INT NOT NULL,
    QuantityAvailable INT NOT NULL,
    QuantityReserved INT NOT NULL,
    LastUpdated DATETIME NOT NULL,
    FOREIGN KEY (ProductID) REFERENCES Products(Id) --ON DELETE CASCADE
);

CREATE TABLE Carts (
    Id INT PRIMARY KEY,
    UserID INT NOT NULL,
    InventoryID INT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(Id), --ON DELETE CASCADE
    FOREIGN KEY (InventoryID) REFERENCES Inventory(Id) --ON DELETE CASCADE
);

CREATE TABLE Orders (
    Id INT PRIMARY KEY,
    UserID INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    TotalAmount FLOAT NOT NULL,
    ShippingAddress NVARCHAR(255) NOT NULL,
    ShippingStatus NVARCHAR(255),
    TrackingNumber NVARCHAR(255),
    UpdatedAt DATETIME NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(Id) --ON DELETE CASCADE
);

CREATE TABLE OrderDetails (
    Id INT PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice FLOAT NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(Id), --ON DELETE CASCADE
    FOREIGN KEY (ProductId) REFERENCES Products(Id) --ON DELETE CASCADE
);

CREATE TABLE Rooms (
    Id INT PRIMARY KEY,
    RoomName NVARCHAR(255) NOT NULL,
    RoomDescription NVARCHAR(MAX) NOT NULL

);

CREATE TABLE Puzzles (
    Id INT PRIMARY KEY,
    Question NVARCHAR(MAX) NOT NULL,
    Answer NVARCHAR(MAX) NOT NULL,
    RoomId INT NOT NULL,
    FOREIGN KEY (RoomId) REFERENCES Rooms(Id)
);

CREATE TABLE PlayerProgress (
    Id INT PRIMARY KEY,
    PlayerName NVARCHAR(255) NOT NULL,
    CurrentRoomId INT NOT NULL,
    FOREIGN KEY (CurrentRoomId) REFERENCES Rooms(Id)
);