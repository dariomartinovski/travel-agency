import pyodbc
import pandas as pd

server = 'server'
database = 'database'
username = 'username'
password = 'password'

connection_string = f'DRIVER={{ODBC Driver 17 for SQL Server}};SERVER={server};DATABASE={database};UID={username};PWD={password}'

conn = pyodbc.connect(connection_string)

cursor = conn.cursor()

cursor.execute("Select * from Books")
books = cursor.fetchall()
columns = [column[0] for column in cursor.description]
books_data = pd.DataFrame.from_records(books, columns=columns)

cursor.execute("Select * from Authors")
authors = cursor.fetchall()
columns = [column[0] for column in cursor.description]
authors_data = pd.DataFrame.from_records(authors, columns=columns)

cursor.execute("Select * from Publishers")
publishers = cursor.fetchall()
columns = [column[0] for column in cursor.description]
publishers_data = pd.DataFrame.from_records(publishers, columns=columns)

# Merge Books with Authors on AuthorId
merged_df = pd.merge(books_data, authors_data, left_on='AuthorId', right_on='Id', suffixes=('', '_Author'))
merged_df.rename(columns={
    'Name': 'AuthorName',
    'LastName': 'AuthorLastName'
}, inplace=True)

# Merge the result with Publishers on PublisherId
final_df = pd.merge(merged_df, publishers_data, left_on='PublisherId', right_on='Id', suffixes=('', '_Publisher'))
final_df.rename(columns={
    'Name': 'PublisherName',
    'Description': 'PublisherDescription'
}, inplace=True)

final_df.drop(columns=["Id_Author", "Id_Publisher", "CreatedById"], inplace=True)
print(final_df.columns)

print(final_df)

final_df.to_csv('final.csv', index=False)

conn.close()
