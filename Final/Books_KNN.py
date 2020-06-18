import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
plt.style.use('ggplot')
#import goodreads_api_client as gr
from sklearn.cluster import KMeans
from sklearn import neighbors
from sklearn.preprocessing import MinMaxScaler
import Helpers



#import the Data
df1 = pd.read_csv('Data/books.csv', error_bad_lines = False)

df1.dataframeName = 'books.csv'
df1.index = df1['bookID']
nRow, nCol = df1.shape
print(f'There are {nRow} rows and {nCol} columns')


df1.replace(to_replace='J.K. Rowling/Mary GrandPré', value = 'J.K. Rowling', inplace=True)

df1['Ratings_Dist'] = Helpers.segregation(df1)

#Convert the categorical ratings distribution to indicator variables
books_features = pd.concat([df1['Ratings_Dist'].str.get_dummies(sep=","), df1['average_rating'], df1['ratings_count']], axis=1)

#reduce bias by scaling and translating each feature individually such that it is in the given range on the training set, e.g. between zero and one.
min_max_scaler = MinMaxScaler()
books_features = min_max_scaler.fit_transform(books_features)

#round array to 2 decimal points
np.round(books_features,2)

#create the knn model, return 7 neighbors/books thus giving us the original + 6 additional
model = neighbors.NearestNeighbors(n_neighbors=7, algorithm='auto')
model.fit(books_features)

#get the distance and index of of each neighbor
distance, indices = model.kneighbors(books_features)

    #print related books based on query
#Helpers.print_similar_books(df1,distance,indices,"Harry Potter and the Half-Blood Prince (Harry Potter  #6)")

    #simply print related books with the id
#Helpers.print_similar_books(df1,distance,indices,id=5107)

    #get a list of related books and return a list of tuple(bookID,userscore, lower the score the closer it relaates)
#Helpers.get_similar_userRated_books(df1,distance,indices,id=1,user_rating=3.5)

    #Print a list of books and their index/bookid relating to the query
#Helpers.get_id_from_partial_name(df1,"Percy J")

#mock user bookid,score tuple, this will be all we need to get the overall reccommendation list for the user
data = [(3,5),(2,3.5),(753,2),(87,4.2)]
userData =Helpers.get_similar_userRated_books_from_list(data,df1,distance,indices)

#Final List of tuple with bookId,title, and overall user 'likeness score' (lower the score the closer the distance in the knn  model and thus the more likely it is a good recommendation)
print(userData)
