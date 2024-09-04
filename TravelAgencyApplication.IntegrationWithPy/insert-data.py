import pyodbc
import pandas as pd

server = 'server'
database = 'database'
username = 'username'
password = 'password'

connection_string = f'DRIVER={{ODBC Driver 17 for SQL Server}};SERVER={server};DATABASE={database};UID={username};PWD={password}'

conn = pyodbc.connect(connection_string)

cursor = conn.cursor()

query = """IF NOT EXISTS (
            SELECT * 
                FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_NAME = 'Books'
            )
            BEGIN
                CREATE TABLE Books (
                    Id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
                    Title VARCHAR(255) NOT NULL,
                    AuthorId uniqueidentifier NOT NULL,
                    PublisherId uniqueidentifier NOT NULL,
                    Price DECIMAL(10, 2) NOT NULL,
                    BookImage TEXT,
                    AuthorName VARCHAR(255),
                    AuthorLastName VARCHAR(255),
                    PublisherName VARCHAR(255),
                    PublisherDescription TEXT
                );
            END
        """

try:
    cursor.execute(query)
    conn.commit()
    print("Table created")
except Exception as e:
    print(f"An error occurred: {e}")


data = pd.read_csv("./final.csv")

insert_query = """
    INSERT INTO dbo.Books (Id, Title, AuthorId, PublisherId, Price, BookImage, AuthorName, AuthorLastName, PublisherName, PublisherDescription)
    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
"""

# Iterate over DataFrame rows and execute the insert statement
for index, row in data.iterrows():
    try:
        cursor.execute(insert_query, (
            row['Id'], 
            row['Title'], 
            row['AuthorId'], 
            row['PublisherId'], 
            row['Price'], 
            row['BookImage'], 
            row['AuthorName'], 
            row['AuthorLastName'], 
            row['PublisherName'], 
            row['PublisherDescription']
        ))

    except Exception as e:
        print(f"An error occurred while inserting row {index}: {e}")
    finally:
        print("Data row inserted successfully")

conn.commit()

conn.close()