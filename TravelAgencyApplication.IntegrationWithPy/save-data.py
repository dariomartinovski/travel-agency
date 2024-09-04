import pandas as pd

data = pd.read_csv("./final.csv")

insert_query = """
    INSERT INTO dbo.Books (Id, Title, AuthorId, PublisherId, Price, BookImage, AuthorName, AuthorLastName, PublisherName, PublisherDescription) 
    VALUES ('{Id}', '{Title}', '{AuthorId}', '{PublisherId}', {Price}, '{BookImage}', '{AuthorName}', '{AuthorLastName}', '{PublisherName}', '{PublisherDescription}');
"""

with open('insert_data.sql', 'w') as file:
    for index, row in data.iterrows():
        try:
            title = row['Title'].replace("'", "''")
            book_image = row['BookImage'].replace("'", "''")
            author_name = row['AuthorName'].replace("'", "''")
            author_last_name = row['AuthorLastName'].replace("'", "''")
            publisher_name = row['PublisherName'].replace("'", "''")
            publisher_description = row['PublisherDescription'].replace("'", "''")

            sql_statement = insert_query.format(
                Id=row['Id'],
                Title=title,
                AuthorId=row['AuthorId'],
                PublisherId=row['PublisherId'],
                Price=row['Price'],
                BookImage=book_image,
                AuthorName=author_name,
                AuthorLastName=author_last_name,
                PublisherName=publisher_name,
                PublisherDescription=publisher_description
            )

            file.write(sql_statement + '\n')

        except Exception as e:
            print(f"An error occurred while processing row {index}: {e}")

print("SQL script has been saved to 'insert_data.sql'.")
