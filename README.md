# RaceDay – PROG6212 Part 2



## What I Did in Part 2

In Part 1, I planned the RaceDay system (ERD, endpoint plan, and SQL script).
In Part 2, I built the actual RESTful API that follows that plan. Here's what I changed and added:

### Updates from Part 1

1. **Added a Results table** to the database.
   My Part 1 ERD and SQL script did not include Results, but my Part 1 endpoint plan did. So I added the Results table to make them match.

2. **Added `CategoryId` to the EventEnrolments table.**
   In Part 1, my EventEnrolments table only had ParticipantId and EventId. I added CategoryId so that when a Participant enrols, the system knows which category they chose.

3. **Created the C# Models** for every table:
   - User, Organiser, Participant, Event, Category, EventEnrolment, Result

4. **Created the database classes** using ADO.NET (the style we learned in class):
   - `store_info.cs` – all INSERT operations
   - `get_user_info.cs` – all SELECT operations
   - `update_user_info.cs` – all UPDATE operations
   - `delete_info.cs` – all DELETE operations

5. **Created 6 API controllers** matching my Part 1 endpoint plan:
   - AuthController (register, login, logout)
   - UsersController (view and update profile)
   - EventsController (create, read, update, delete events)
   - CategoriesController (create, read, update, delete categories)
   - EnrolmentsController (Participant enrols, Organiser views)
   - ResultsController (Organiser records, anyone views)

6. **Added session-based login.**
   When a user logs in, the API stores their User ID and Role in the session. Every protected endpoint then checks the session to enforce role-based access.

7. **Hashed passwords with BCrypt.**
   Passwords are never stored as plain text.

8. **Added Swagger descriptions.**
   Every endpoint has an XML comment so Swagger shows what it does, what it expects, and what it returns.

9. **Wrote 12 unit tests** using xUnit:
   - Password hashing and verification
   - Role validation
   - Event type validation
   - Model defaults

10. **Set up GitHub Actions CI/CD** to build the project and run tests on every push.



## How to Run

1. Open `RaceDay.sln` in Visual Studio 2022.
2. In SSMS, connect to `(localdb)\api_guide` and run `docs/RACEDAY_database.sql`.
3. Press **F5**.
4. Swagger opens. Register an Organiser and a Participant, then start using the endpoints.

The database starts empty — all data is added through Swagger.



