# WebFlightControl

## Table of contents
* [General info](#general-info)
* [More about the project](#more-about-the-project)

## General info
In this project, we built a web application using ASP.NET core.
This application is a flights control system, showing flights routes on a world map and providing live information about the current active flight plans.
In addition to the ability to track flights from this project server, the application can connect to other external servers and get information about their own flights.
The client side is written with JavaScript and designed with Bootstrap. The server side is written with C#.

## More about the project
Unit test:
We did 2 test on the method getflightplan in flight plan controller.
Test1 :
Mock object-externalflight class .
Sub-the sql command the db .
The test check that it return the right flight plan from db and not calling the method externalflight.

Test 2:
Mock and sub are the same.
We implement the mock func that return the flight plan
The test check that db didnt have it ,the right flight plan return and the method external flight called once.

## Screenshots
![main](https://user-images.githubusercontent.com/58934116/110238347-d81c3700-7f49-11eb-990d-d4b72fe64626.jpg)

![external](https://user-images.githubusercontent.com/58934116/110238395-1580c480-7f4a-11eb-8074-b8b2238af729.jpg)

## Contact
Created by [@raphaelHouri](https://github.com/raphaelHouri) [@meshiBiton](https://github.com/meshibiton)- feel free to contact us! :mailbox_closed:
