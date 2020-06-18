import numpy as np
import pandas as pd
import os
import seaborn as sns
#import isbnlib
#from newspaper import Article
import matplotlib.pyplot as plt
plt.style.use('ggplot')
from tqdm import tqdm
import re
from scipy.cluster.vq import kmeans, vq
from pylab import plot, show
from matplotlib.lines import Line2D
import matplotlib.colors as mcolors
#import goodreads_api_client as gr
from sklearn.cluster import KMeans
from sklearn import neighbors
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import MinMaxScaler
from sklearn.preprocessing import StandardScaler
from mpl_toolkits.mplot3d import Axes3D
from RecommendedBook import RecommendedBook

# Distribution graphs (histogram/bar graph) of column data
def plotPerColumnDistribution(df, nGraphShown, nGraphPerRow):
    nunique = df.nunique()
    df = df[[col for col in df if nunique[col] > 1 and nunique[col] < 50]] # For displaying purposes, pick columns that have between 1 and 50 unique values
    nRow, nCol = df.shape
    columnNames = list(df)
    nGraphRow = (nCol + nGraphPerRow - 1) / nGraphPerRow
    plt.figure(num = None, figsize = (6 * nGraphPerRow, 8 * nGraphRow), dpi = 80, facecolor = 'w', edgecolor = 'k')
    for i in range(min(nCol, nGraphShown)):
        plt.subplot(nGraphRow, nGraphPerRow, i + 1)
        columnDf = df.iloc[:, i]
        if (not np.issubdtype(type(columnDf.iloc[0]), np.number)):
            valueCounts = columnDf.value_counts()
            valueCounts.plot.bar()
        else:
            columnDf.hist()
        plt.ylabel('counts')
        plt.xticks(rotation = 90)
        plt.title(f'{columnNames[i]} (column {i})')
    plt.tight_layout(pad = 1.0, w_pad = 1.0, h_pad = 1.0)
    plt.show()


# Correlation matrix
def plotCorrelationMatrix(df, graphWidth):
    filename = df.dataframeName
    df = df.dropna('columns') # drop columns with NaN
    df = df[[col for col in df if df[col].nunique() > 1]] # keep columns where there are more than 1 unique values
    if df.shape[1] < 2:
        print(f'No correlation plots shown: The number of non-NaN or constant columns ({df.shape[1]}) is less than 2')
        return
    corr = df.corr()
    plt.figure(num=None, figsize=(graphWidth, graphWidth), dpi=80, facecolor='w', edgecolor='k')
    corrMat = plt.matshow(corr, fignum = 1)
    plt.xticks(range(len(corr.columns)), corr.columns, rotation=90)
    plt.yticks(range(len(corr.columns)), corr.columns)
    plt.gca().xaxis.tick_bottom()
    plt.colorbar(corrMat)
    plt.title(f'Correlation Matrix for {filename}', fontsize=15)
    plt.show()

    # Scatter and density plots
def plotScatterMatrix(df, plotSize, textSize):
    df = df.select_dtypes(include=[np.number])  # keep only numerical columns
    # Remove rows and columns that would lead to df being singular
    df = df.dropna('columns')
    df = df[[col for col in df if df[col].nunique() > 1]]  # keep columns where there are more than 1 unique values
    columnNames = list(df)
    if len(columnNames) > 10:  # reduce the number of columns for matrix inversion of kernel density plots
        columnNames = columnNames[:10]
    df = df[columnNames]
    ax = pd.plotting.scatter_matrix(df, alpha=0.75, figsize=[plotSize, plotSize], diagonal='kde')
    corrs = df.corr().values
    for i, j in zip(*plt.np.triu_indices_from(ax, k=1)):
        ax[i, j].annotate('Corr. coef = %.3f' % corrs[i, j], (0.8, 0.2), xycoords='axes fraction', ha='center',
                          va='center', size=textSize)
    plt.suptitle('Scatter and Density Plot')
    plt.show()

def segregation(data):
    values = []
    for val in data.average_rating:
        if val >= 0 and val <= 1:
            values.append("Between 0 and 1")
        elif val > 1 and val <= 2:
            values.append("Between 1 and 2")
        elif val > 2 and val <= 3:
            values.append("Between 2 and 3")
        elif val > 3 and val <= 4:
            values.append("Between 3 and 4")
        elif val > 4 and val <= 5:
            values.append("Between 4 and 5")
        else:
            values.append("NaN")
    print(len(values))
    return values


def get_index_from_name(df,name):
    return df[df["title"] == name].index.tolist()[0]

def get_id_from_partial_name(df,partial):
    all_books_names = list(df.title.values)
    for name in all_books_names:
        if partial in name:
            print(name, all_books_names.index(name))


def print_similar_books(df,distance,indices,query=None, id=None):
    if id:
        for idx, id in enumerate(indices[id][1:]):
            print(df.iloc[id]["title"],
                  " [ID: {bid}] - [Distance: {dis}]".format(bid=df.iloc[id]["bookID"], dis=distance[id][idx]))
    if query:
        found_id = get_index_from_name(df,query)
        for idx, id in enumerate(indices[found_id][1:]):
            print(df.iloc[id]["title"],
                  " [ID: {bid}] - [Distance: {dis}]".format(bid=df.iloc[id]["bookID"], dis=distance[id][idx]))


# This will return a user score based off a rating the user gave the book and the distance of how closely related the books are to one another, the smaller the user score the moore liekly the user will enjoy the book
def get_similar_userRated_books(df,distance,indices,query=None, id=None, user_rating=1):
    userData = []
    if id:
        for idx, id in enumerate(indices[id][1:]):
            bookID = df.iloc[id]["bookID"]
            userScore = distance[id][idx] / user_rating
            data = (bookID, userScore)
            print(df.iloc[id]["title"],
                  " [ID: {bid}] - [Distance: {dis}]".format(bid=df.iloc[id]["bookID"], dis=distance[id][idx]))
            print("id: {}  -  score: {}".format(bookID, userScore))
            userData.append(data)
    if query:
        found_id = get_index_from_name(df,query)
        for idx, id in enumerate(indices[found_id][1:]):
            bookID = df.iloc[id]["bookID"]
            userScore = distance[id][idx] / user_rating
            print(df.iloc[id]["title"],
                  " [ID: {bid}] - [Distance: {dis}]".format(bid=df.iloc[id]["bookID"], dis=distance[id][idx]))
            print("id: {}  -  score: {}".format(bookID, userScore))
            userData.append(data)
    print(userData)
    return userData


def get_similar_userRated_books_from_list(userRateData,df,distance,indices):
    userData = []
    for (bookID, score) in userRateData:
        for idx, id in enumerate(indices[bookID][1:]):
            bookID = df.iloc[id]["bookID"]
            userScore = distance[id][idx] / score
            data = (bookID, userScore)
            userData.append(data)
            sortedData = sorted(userData, key=lambda tup: tup[1])
    return get_books_from_userScore(sortedData,df)


def get_books_from_userScore(userData,df):
    data = []
    for (bookID, score) in userData:
        print(bookID, ":", df.loc[bookID].title, " - ", score)
        data.append(RecommendedBook(bookID, df.loc[bookID].title, score))
        # return the list of tuples containing id,title,and score all already ordered
    return data
