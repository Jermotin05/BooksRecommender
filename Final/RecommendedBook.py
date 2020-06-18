class RecommendedBook:
    def __init__(self,bookID,name,distance):
        self.BookID = bookID
        self.BookName = name
        self.BookDistance = distance

    def to_dict(self):
        return {"BookID":self.BookID,"BookName":self.BookName,"BookDistance":self.BookDistance}
