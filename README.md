# Book Recommender
- Southern New Hamphshire University
- CS 499 Final project
- 06/21/2020

## Professional Self Asseessment
While at Southern New Hampshire University (SNHU) I’ve had the privilege to work on several fun and challenging projects. SNHU has given me the chance to explore areas of computer science such as NOSQL databases and data mining that I’ve never really considered. While I’ve worked as a professional .Net developer for 2+ years now I was still able to learn new things everyday from SNHU and their collection of skilled and knowledgeable professors. This final capstone project I wanted to incorporate a mixture of my skills learned from work on the .Net side with a combination of python, NoSQL, and data mining that SNHU has allowed me to explore and grow into. While at SNHU I’ve also been exposed to a new form of collaboration and communication that is rare for a university. From the online nature of this program, I was often exposed to just how a team must communicate when working collaboratively but remotely.  Communication with professors and classmates was paramount to success. One of the greatest things I learned from SNHU was there is never just one way to do something. Professors here have always given room for creative license and allowed just enough freedom for me to follow my curiosities. Overall, my time here at SNHU has been very empowering and has taught me so much more than just programming. Its taught me about design, testing, communication and how to follow and execute business requirements to deliver a successful product. 

This final project really showcases my strengths by demonstrating not only how I can build a basic functional website with services and a dedicated data-mining API but also how I can take an idea and build a solution around it. While I’ve built many APIs before it has always been language specific. This project challenged me to build a dedicated data mining API that can be called from my .Net application. This application is meant to be a basic example of a custom recommendation system using unsupervised learning and K-means clustering. The dataset was provided from Kaggle.com and comes from the Goodreads API. My application is designed to allow a user to register, login and view and mark books as read. They also can view books and see similar books based on the Euclidean distance determined from the clustering algorithm. This distance allows me to determine how similar each book is based on other books in the dataset, a full exploratory data analysis report will also be provided in the form of a Juypter notebook. 

The UI of this application is built using ASP.Net Core Blazor framework. Blazor is a new framework from Microsoft that allows C# code to be execute in the browser using WebAssembly. This application was largely written in C# because I wanted to explore how I could use python for machine learning and still bring those models results over to a .Net application. While there are many options for this, and I could have used ML.Net I really wanted to demonstrate how we can connect these languages through an API and simple JSON serialization. This applications API is small and simple, it serves as a gateway to transfer the clustering models data to and from the app. While I could have built another API to allow for Blazor calls such as a method to get books. I wanted this API to work only as a data-mining API. For all other calls I’m still using the same database but I’m working with Blazor services to accomplish this task. My choice of a database came down to Microsoft SQL Server and MongoDB. I choose to use MongoDB since it’s a NoSQL database and supports unstructured data. Since my dataset is relatively small and I wanted this database to support both basic web app functionality as well as data mining this was a great fit. Overall, this application was written to demonstrate understanding and knowledge in the domains of web apps, APIs, data mining and data analytics. While  I believe I was able to demonstrate competency in these areas I recognize there is plenty room for improvement on this project, such as using an API to get the data rather than a static dataset, or more in-depth feature engineering of the dataset to provide more accurate recommendations. 


## Code Review 
A full [Code Review](https://youtu.be/FoAmb9bHvag) detailing where I began on each artifact and my plans to improve each item.

## Artifacts
### Design & Engineering
This was a simple blazor app that was rapidly created to display a list of books the user has read, a list of all books with custom pagination. This has a very basic login and register page yet there is no authoprization and very basic authentication. The mian goal of this was to give a UI that would work with the clustering model to actually work as a delivery platofrm. While its not the best looking it serves its purpose for this project.
![login](https://user-images.githubusercontent.com/26797544/85058392-1087a500-b170-11ea-897c-da79d7fec37a.PNG)
![index](https://user-images.githubusercontent.com/26797544/85058473-2b5a1980-b170-11ea-9034-9f0e18c0a100.PNG)
![books](https://user-images.githubusercontent.com/26797544/85058506-38770880-b170-11ea-8763-7136643680bc.PNG)
![details](https://user-images.githubusercontent.com/26797544/85058516-3a40cc00-b170-11ea-9d9e-4edd807f7974.PNG)

### Algorithms and Data Structure
The initial idea behind this project came from the goodreadsbooks dataset from [kaggle](https://www.kaggle.com/). Afteusing some started code I went ahead and begin basic EDA and design of my clustering model to help recommend similar books based on the euclidian distance of each node in my clustering model. The full jupyter notebook can be found in my [repo](https://github.com/Jermotin05/BooksRecommender/blob/master/Starter_%20Goodreads.ipynb)
![clusters](https://user-images.githubusercontent.com/26797544/85059079-0ca85280-b171-11ea-8e67-f79860ceba82.PNG)

### Databases

## Narratives
### Design & Engineering
##### [Design Narrative](https://youtu.be/Ngj79AbDDGk)
### Algorithms
##### [Algorithms Narrative](https://youtu.be/--y6VB1QG1U)
### Databases
Artifact three is a python API written using the bottle library. This APIs job is to run data intensive machine learning and data analytics models and then return the data in a json format. This API is supposed to be small and simply a bridge to join .Net and python together. This allows the creation of .Net web applications to be integrated with machine learning and data analysis projects from python. While a similar clustering model could have been made in .Net, python is one of the best languages to use for this. This artifact runs the clustering model to find similar books and book recommendations and then returns these results via json. This API showcases database interaction in a few ways. While there actually isn’t a ton of database calls in this application, the actual creation of an API and being able to get data, store data and transfer this data around is really what this artifact is all about. To me being able to not only make database calls but to actually handle the data is also critical. 

This artifact was one I was a little worried about but overall, it went great and I learned a lot. While I’ve made plenty of APIs, I’ve never made one that crosses languages so creating an API to in python to communicate with C# was a little difficult to get it working. I was able to get json data from my python methods pretty quickly and wire that all up with my clustering model with very little problems. Some of the issues I ran into was ensuring I could call the API from C# and de-serialize the json into C# objects. This took a little while to figure out, even using postman to test my api calls I could see I was getting back valid json but the actual structure didn’t match how it should and it turned out I was converting each item in a list to a json object and then converting the entire list into a json object and this was causing some major issues. After finding the solution to this it was pretty easy to get everything wired up and running. Another issue I had was getting the book rating data for the user. I at first had this data mocked up and was able to create my clustering model on the mock data but for the api I knew I wanted to actually pull the users rating of each book from the db and then run the clustering model on this data instead of mock data. To get this I had to go back to the blazor app and change the ui to let the user enter and update a rating for each book, while I was doing this I also added a book details page to take care of the other issue of where to show similar books instead of recommended books. 





