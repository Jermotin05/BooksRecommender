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
### Algorithms and Data Structure
### Databases

## Narratives
##### [Design Narrative](https://youtu.be/Ngj79AbDDGk)
##### [Algorithms](https://youtu.be/--y6VB1QG1U)



