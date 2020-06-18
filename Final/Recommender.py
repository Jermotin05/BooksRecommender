import numpy as np
import pandas as pd
from sklearn import neighbors
from sklearn.preprocessing import MinMaxScaler
import Helpers
import json
from RecommendedBook import RecommendedBook

class Recommender:
    N_NEIGHBORS = 7

    # class variable describing the dataframe
    df = pd.DataFrame()
    books_features = pd.DataFrame()
    model = neighbors.NearestNeighbors()
    distance = None
    indices = None
    user_book_data = [(3,5),(2,3.5),(753,2),(87,4.2)]
    recommendations = None

    def getDatFrame(self):
        df1 = pd.read_csv('Data/books.csv', error_bad_lines = False)
        df1.dataframeName = 'books.csv'
        df1.index = df1['bookID']
        df1['Ratings_Dist'] = Helpers.segregation(df1)
        self.df = df1

    def getBook_features(self):
        self.books_features = pd.concat([self.df['Ratings_Dist'].str.get_dummies(sep=","), self.df['average_rating'], self.df['ratings_count']], axis=1)
        # reduce bias by scaling and translating each feature individually such that it is in the given range on the training set, e.g. between zero and one.
        min_max_scaler = MinMaxScaler()
        books_features = min_max_scaler.fit_transform(self.books_features)
        np.round(books_features, 2)

    def getKNN(self):
        self.model = neighbors.NearestNeighbors(n_neighbors=self.N_NEIGHBORS, algorithm='auto')
        self.model.fit(self.books_features)
        self.distance, self.indices = self.model.kneighbors(self.books_features)

    def getRelatedBooks(self,query=None, id=None):

        data = []
        if id:
            for idx, id in enumerate(self.indices[id][1:]):
                if self.df.iloc[id]["bookID"] is not None:
                    data.append(RecommendedBook(int(self.df.iloc[id]["bookID"]), self.df.iloc[id]["title"], self.distance[id][idx]))
                    #data.append((self.df.iloc[id]["bookID"], self.df.iloc[id]["title"], self.distance[id][idx]))
                    print(self.df.iloc[id]["title"]," [ID: {bid}] - [Distance: {dis}]".format(bid=self.df.iloc[id]["bookID"], dis=self.distance[id][idx]))
        if query:
            found_id = Helpers.get_index_from_name(self.df, query)
            for idx, id in enumerate(self.indices[found_id][1:]):
                if self.df.iloc[id]["bookID"] is not None:
                    data.append(RecommendedBook(int(self.df.iloc[id]["bookID"]), self.df.iloc[id]["title"], self.distance[id][idx]))
                    #data.append((self.df.iloc[id]["bookID"], self.df.iloc[id]["title"], self.distance[id][idx]))
                    print(self.df.iloc[id]["title"]," [ID: {bid}] - [Distance: {dis}]".format(bid=self.df.iloc[id]["bookID"], dis=self.distance[id][idx]))
        return data

    def getBookRecommendation(self):
        userData = []
        for (bookID, score) in self.user_book_data:
            for idx, id in enumerate(self.indices[bookID][1:]):
                bookID = int(self.df.iloc[id]["bookID"])
                userScore = self.distance[id][idx] / score
                data = (bookID, userScore)
                userData.append(data)
                sortedData = sorted(userData, key=lambda tup: tup[1])
        self.recommendations = Helpers.get_books_from_userScore(sortedData, self.df)


r = Recommender()
r.getDatFrame()
r.getBook_features()
r.getKNN()
r.getRelatedBooks(id=1)
r.getBookRecommendation()
for r in r.recommendations:
    print(r.BookName)

#for book in t:
 #   print(book.BookName)

