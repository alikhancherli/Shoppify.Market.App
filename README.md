# Shoppify.Market.App.Api
In this project I tried to privide a view as the best practice for those who want to keep learning and do some examples.
<h2>🏚 Project architecture :</h2>
<p>
Project based on (Onion Architecture) with JWT identity in order to authenticate with Json Web Tokens. Additionally, In the project I followedDD definitons.
</p>

<h2>
Domain Layer :
</h2>
<p>
Domain layer includes all of the models that we need in the project. Like Users,roles,products and product categories. Of course as the identity I implement the AspNetIdentity as well.
</p>

<h2>
Persistence Layer :
</h2>
<p>
You can do all of the database things in this layer.
</p>

<h2>
Service Layer:
</h2>
<p>
Service layer used for implementing CQRS pattern and validators like Fluent Validation and stuff. Also dtos and resources are in this layer.
</p>

<h2>
Identity Layer :
</h2>
<p>
Find identity options or extensions for develope more and more in identity layer.
</p>

<h2>
Infrastructure Layer:
</h2>
<p>
For making the configurations that you know you'll need in multiple projects, its cool to put you things here and use them in several project as presentation layer.
</p>
# Unit tests
There are some unit test (not much but enough) and if you want to know how you can write unit tests, it's ok to take a look at this layer
