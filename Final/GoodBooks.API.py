
#!/usr/bin/python
import datetime
import json
from bson import json_util
import bottle
from bottle import route, run, get, post,request, abort, template, static_file,response
from wtforms.validators import ValidationError
from pymongo import MongoClient
import Recommender as re

def DBConnection(database='GoodBooks',collection='Users'):
    connection = MongoClient("mongodb+srv://School:CS499Password@jscluster0-tml9y.mongodb.net/test?retryWrites=true&w=majority")
    db = connection[database]
    collection = db[collection]
    return collection


#Get Recommended books based on the book id
@route('/getSimilarBooks/<bookID>',method='GET')
def GetSimilarBooks(bookID):
    try:
        #check ticker is valid
        if bookID is None:
            raise ValueError("Must have a valid book id")

        #show the query is working and create the query
        print("Looking for document,id: {}" .format(bookID))

        rec = re.Recommender()
        rec.getDatFrame()
        rec.getBook_features()
        rec.getKNN()
        data = rec.getRelatedBooks(id=1)
        resultList = []
        for obj in data:
            resultList.append(obj.__dict__)
        bottle.response.status = 200

    except ValidationError as ve:
        response.status = 404
        abort(400, str(ve))
    return json.dumps(resultList)


#Get Recommended books based on the book id
@route('/getRecommendations/<userID>',method='GET')
def GetRecommendations(userID):
    try:
        #check ticker is valid
        if userID is None:
            raise ValueError("Must have a valid user id")

        rec = re.Recommender()
        rec.getDatFrame()
        rec.getBook_features()
        rec.getKNN()

        #rec.user_book_data = [(3,5),(2,3.5),(753,2),(87,4.2)]
        rec.user_book_data = GetUserRatings(userID)
        print("bookrating",rec.user_book_data)

        rec.getBookRecommendation()
        resultList = []
        for obj in rec.recommendations:
            resultList.append(obj.__dict__)
        print("result",resultList)
        bottle.response.status = 200

    except ValidationError as ve:
        response.status = 404
        abort(400, str(ve))

    return json.dumps(resultList)



@route('/getUserRatings/<userID>',method='GET')
def GetUserRatings(userID):
    try:
        #check ticker is valid
        if userID is None:
            raise ValueError("Must have a valid user id")

        ratingData = []
        query = {"UserID": userID, "Rating":{"$exists":"True"}}
        result = DBConnection().find(query)
        for data in result:
            ratingData.append((int(data["BookID"]),int(data["Rating"])))
            print("data",data)

        bottle.response.status = 200
        print("ratingData", ratingData)

    except ValidationError as ve:
        response.status = 404
        abort(400, str(ve))

    return ratingData





if __name__ == '__main__':
 #app.run(debug=True)
 run(host='localhost', port=8080)