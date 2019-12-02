# Instructions/Assumptions

Below is the folder structure in the project.

- Core
  - Interfaces
    - All the Interfaces have been specified here. In this case ICustomerRepository which is the abstract implementation of DB operations.
  - Filters
    - Application level filters have been specified here. In our example it is the API level validation has been handled here.
  - Infrastructure
    - DB Context and the sample data generation has been placed here.
  - Messages
    - Both error and information messages for the whole application have been specified here. This is the central place to add/modify/delete any error/information messages.
  - Models
    - Both Domain models and View Models have been defined here.
  - Options
    - Swagger options have been specified here.
  - Repositories
    - Implementation logic relating to DB operations have been placed here.
  - Responses
    - API response types have been handled here.
    
  Swagger end-point can be accessed using URL: http://localhost:5000/swagger. Actual application can be accessed using the URL: http://localhost:5000
   
