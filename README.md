# _Hair Salon_

#### _A hair salon information website, August 18, 2017_

#### By _**Robert Murray**_

## Description

_A website that stores the information of hair stylists and their clients._

|| Behavior  | Input  | Output  |
|---|---|---|---|
|1.| Saves input stylist to database and assigns unique id  |   |   |
|2.| Retrieves all stylists from database  |   |   |
|3.| Saves input client to database and assigns both a unique id and the related stylist id  |   |   |
|4.| Retrieves list of clients for a specific stylist from the database |   |   |
|5.| Retrieves information from a specific client from the database |   |   |
|6.| Ability to update client name in database  |   |   |
|7.| Ability to delete a client from the database  |   |   |
|8.| Ability to delete a stylist from the database  |   |   ||



## Setup/Installation Requirements

* _Clone repository_
* _Download and install [.NET Core 1.1 SDK](https://www.microsoft.com/net/download/core)_
* _Download and install [Mono](http://www.mono-project.com/download/)_
* _Download and install [MAMP](https://www.mamp.info/en/)_
* _Open MAMP port connection to 8889_

* _In MySQL:_

  **>** CREATE DATABASE hair_salon;
<br>
**>** USE hair_salon;
<br>
**>** CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
<br>
**>** CREATE TABLE clients (id serial PRIMARY KEY, description VARCHAR(255), stylist_id INT);


## Technologies Used
* _C#_
* _.NET_
* _[Bootstrap](http://getbootstrap.com/getting-started/)_
* _[mySQL](https://www.mysql.com/)_

### License

Copyright (c) 2017 **_Robert Murray_**

*Licensed under the [MIT License](https://opensource.org/licenses/MIT)*
