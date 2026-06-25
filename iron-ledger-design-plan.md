# Iron Ledger

**Project Subtitle:** A .NET gRPC microservice system for managing powerlifting meets, attempts, scoring, and leaderboards.

---

## 1. Project Overview

Iron Ledger is a powerlifting meet management system designed as a learning project for .NET microservice architecture and gRPC communication.

The system allows meet organizers to create meets, register athletes, enter lifting attempts, calculate totals, and display leaderboards. The backend is divided into multiple .NET services that communicate through gRPC. A lightweight React frontend provides a simple administrative dashboard.

The main purpose of this project is not to build a production-ready powerlifting platform. The main purpose is to practice the structure, design, debugging, and development workflow of a .NET-based microservice system.

---

## 2. Learning Goals

The project is designed to develop the following skills:

- Building services with .NET and C#
- Designing service boundaries
- Defining gRPC contracts
- Using one service to call another service
- Building an API Gateway / Backend-for-Frontend layer
- Connecting a React frontend to a .NET backend
- Modeling domain entities
- Handling validation and service errors
- Writing unit and integration tests
- Adding logging and health checks
- Running multiple services locally
- Explaining a distributed backend architecture clearly

---

## 3. Project Scope

### In Scope

The first full version of Iron Ledger will support:

- Creating meets
- Registering athletes
- Assigning athletes to meets
- Tracking squat, bench, and deadlift attempts
- Recording attempt results
- Calculating best lifts
- Calculating totals
- Displaying leaderboards
- Basic React dashboard
- REST API Gateway for frontend communication
- Internal gRPC service-to-service communication
- Basic persistence
- Basic validation
- Basic testing and logging

### Out of Scope for Initial Version

The first version will not include:

- Real user authentication
- Payment processing
- Public athlete profiles
- Real email or SMS notifications
- Advanced meet scheduling
- Real-time referee lights
- Federation-specific rules
- Full DOTS/IPF GL scoring accuracy
- Complex React design system
- Cloud deployment

These can be added later as stretch goals.

---

## 4. Target Users

### Primary User

The primary user is a meet director or meet administrator.

This user needs to:

- Create meets
- Register athletes
- Enter attempts
- Update attempt results
- View leaderboards

### Secondary User

The secondary user is a spectator, athlete, or coach.

This user would mainly view:

- Athlete attempts
- Current standings
- Final results

The first version will focus mostly on the meet administrator.

---

## 5. Core User Flows

### Flow 1: Create a Meet

1. Admin opens the dashboard.
2. Admin creates a new meet.
3. Admin enters meet name, date, and location.
4. System saves the meet.
5. Meet appears in the meets list.

### Flow 2: Register an Athlete

1. Admin selects a meet.
2. Admin adds a new athlete.
3. Admin enters athlete name, bodyweight, weight class, division, and team.
4. System saves the athlete.
5. Athlete appears in the athlete list.

### Flow 3: Enter Attempts

1. Admin selects an athlete.
2. Admin selects lift type: squat, bench, or deadlift.
3. Admin enters attempt number and weight.
4. Attempt is saved as pending.
5. Admin later marks the attempt as good lift or no lift.

### Flow 4: Calculate Total

1. System retrieves all attempts for an athlete.
2. System finds the best successful squat.
3. System finds the best successful bench.
4. System finds the best successful deadlift.
5. System adds the three best lifts together.
6. System returns the athlete’s total.

### Flow 5: View Leaderboard

1. Admin opens leaderboard page.
2. Frontend requests leaderboard from the API Gateway.
3. API Gateway calls the Leaderboard Service.
4. Leaderboard Service gathers data from other services.
5. System returns ranked athletes.
6. React displays the leaderboard.

---

## 6. High-Level Architecture

Iron Ledger uses a frontend/backend split.

The React frontend communicates with a .NET API Gateway using normal HTTP requests. The API Gateway communicates with internal backend services using gRPC.

### Architecture

React Frontend  
→ API Gateway / Backend-for-Frontend  
→ gRPC Services  
→ Service Databases

### Internal Services

- Athlete Service
- Meet Service
- Attempt Service
- Scoring Service
- Leaderboard Service

The frontend should not communicate directly with gRPC services. This keeps the frontend simple and keeps gRPC as an internal backend concern.

---

## 7. Service Overview

## 7.1 API Gateway

### Responsibility

The API Gateway is the entry point for the React frontend.

It exposes frontend-friendly HTTP endpoints and translates those requests into gRPC calls.

### Owns Data?

No. The API Gateway should not own core business data.

### Responsibilities

- Accept requests from React
- Validate basic request shape
- Call internal gRPC services
- Combine responses when needed
- Return frontend-friendly responses
- Hide internal service details from the frontend

### Example Features

- Create athlete
- List athletes
- Create meet
- Submit attempt
- Get leaderboard

---

## 7.2 Athlete Service

### Responsibility

The Athlete Service owns athlete information.

### Owns Data?

Yes.

### Main Data

Athlete:

- Athlete ID
- Name
- Bodyweight
- Weight class
- Division
- Team name
- Meet ID

### Responsibilities

- Create athletes
- Update athletes
- Retrieve athletes
- List athletes for a meet
- Validate athlete-related information

### Does Not Handle

- Attempts
- Scoring
- Leaderboards
- Meet creation

---

## 7.3 Meet Service

### Responsibility

The Meet Service owns meet information.

### Owns Data?

Yes.

### Main Data

Meet:

- Meet ID
- Name
- Date
- Location
- Status

Possible meet statuses:

- Draft
- Active
- Completed
- Cancelled

### Responsibilities

- Create meets
- Retrieve meets
- List meets
- Update meet status
- Store meet-level information

### Does Not Handle

- Athlete attempt results
- Scoring
- Leaderboards

---

## 7.4 Attempt Service

### Responsibility

The Attempt Service owns lift attempts.

### Owns Data?

Yes.

### Main Data

Attempt:

- Attempt ID
- Athlete ID
- Meet ID
- Lift type
- Attempt number
- Weight
- Result

Lift types:

- Squat
- Bench
- Deadlift

Attempt results:

- Pending
- Good lift
- No lift

### Responsibilities

- Submit attempts
- Update attempt result
- Retrieve attempts for an athlete
- Retrieve attempts for a meet
- Validate attempt number, weight, lift type, and result

### Does Not Handle

- Athlete personal information
- Meet details
- Leaderboard ranking

---

## 7.5 Scoring Service

### Responsibility

The Scoring Service calculates athlete totals and scores.

### Owns Data?

Usually no, at least in the first version.

The Scoring Service can be stateless. It receives data, performs calculations, and returns results.

### Main Calculations

For each athlete:

- Best squat
- Best bench
- Best deadlift
- Total

Optional later:

- DOTS-style score
- Team score
- Best lifter score

### Responsibilities

- Calculate best lifts
- Calculate total
- Handle missing lifts
- Return score summary

### Does Not Handle

- Storing attempts
- Creating athletes
- Ranking full leaderboards

---

## 7.6 Leaderboard Service

### Responsibility

The Leaderboard Service builds rankings.

### Owns Data?

Not in the first version.

It may be stateless and use data from other services.

### Responsibilities

- Get athletes in a meet
- Get attempts for those athletes
- Call Scoring Service
- Rank athletes by total
- Return leaderboard results

### Leaderboard Types

Initial version:

- Overall leaderboard

Later versions:

- By weight class
- By division
- By team
- By formula score

---

## 8. Service Communication

### Frontend to Backend

React communicates with the API Gateway through HTTP.

The frontend should only know about the API Gateway.

### Backend to Backend

Backend services communicate through gRPC.

### Example Leaderboard Request

1. React requests meet leaderboard.
2. API Gateway receives request.
3. API Gateway calls Leaderboard Service.
4. Leaderboard Service calls Athlete Service for athletes.
5. Leaderboard Service calls Attempt Service for attempts.
6. Leaderboard Service calls Scoring Service for totals.
7. Leaderboard Service returns ranked results.
8. API Gateway returns response to React.

---

## 9. Data Ownership

Each service should own its own data.

### Athlete Service Owns

- Athlete profiles
- Athlete bodyweight
- Athlete division
- Athlete weight class
- Athlete team

### Meet Service Owns

- Meet name
- Meet date
- Meet location
- Meet status

### Attempt Service Owns

- Lift attempts
- Attempt weights
- Attempt results

### Scoring Service Owns

- No persistent data in the first version

### Leaderboard Service Owns

- No persistent data in the first version

---

## 10. Database Strategy

### First Version

Use one simple local database or in-memory storage while learning the domain.

### Better Microservice Version

Eventually, each stateful service should have its own database:

- Athlete Service database
- Meet Service database
- Attempt Service database

Scoring Service and Leaderboard Service can remain stateless at first.

This teaches the microservice principle that services should not directly read each other’s databases. Instead, they should communicate through service APIs.

---

## 11. React Frontend Design

The React frontend should remain intentionally small.

### Pages

### Meets Page

Purpose:

- View meets
- Create a meet
- Select active meet

### Athletes Page

Purpose:

- View athletes in a meet
- Add athlete
- View athlete details

### Attempt Entry Page

Purpose:

- Select athlete
- Enter attempt
- Update attempt result

### Leaderboard Page

Purpose:

- View ranked athletes
- Show best squat, bench, deadlift, and total

### Frontend Rule

React should not know how the internal services are structured.

React only talks to the API Gateway.

---

## 12. Main Entities

### Athlete

Represents a lifter.

Fields:

- ID
- Name
- Bodyweight
- Weight class
- Division
- Team
- Meet ID

### Meet

Represents a powerlifting meet.

Fields:

- ID
- Name
- Date
- Location
- Status

### Attempt

Represents one lift attempt.

Fields:

- ID
- Athlete ID
- Meet ID
- Lift type
- Attempt number
- Weight
- Result

### Score Summary

Represents calculated scoring information for an athlete.

Fields:

- Athlete ID
- Best squat
- Best bench
- Best deadlift
- Total
- Optional formula score

### Leaderboard Entry

Represents one row in a leaderboard.

Fields:

- Rank
- Athlete ID
- Athlete name
- Weight class
- Division
- Best squat
- Best bench
- Best deadlift
- Total

---

## 13. Validation Rules

### Athlete Validation

- Name is required.
- Bodyweight must be positive.
- Weight class is required.
- Division is required.
- Meet ID is required.

### Meet Validation

- Name is required.
- Date is required.
- Location is optional but recommended.
- Status must be valid.

### Attempt Validation

- Athlete ID is required.
- Meet ID is required.
- Lift type must be squat, bench, or deadlift.
- Attempt number must be 1, 2, or 3.
- Weight must be positive.
- Result must be pending, good lift, or no lift.

### Business Rules

- Each athlete can have at most three squat attempts.
- Each athlete can have at most three bench attempts.
- Each athlete can have at most three deadlift attempts.
- Only good lifts count toward the total.
- A missing good lift counts as zero for that lift.
- An athlete’s total is best squat plus best bench plus best deadlift.

---

## 14. Error Handling Strategy

Errors should be handled clearly at two levels.

### Internal gRPC Errors

Services should return meaningful errors for:

- Invalid request
- Missing athlete
- Missing meet
- Duplicate attempt
- Invalid attempt number
- Internal service failure

### API Gateway Errors

The API Gateway should translate internal service errors into frontend-friendly HTTP responses.

Example user-facing error messages:

- “Athlete not found.”
- “Attempt weight must be greater than zero.”
- “Attempt number must be 1, 2, or 3.”
- “Unable to load leaderboard right now.”

---

## 15. Testing Strategy

### Unit Tests

Unit tests should focus on pure logic.

Important unit tests:

- Best squat calculation
- Best bench calculation
- Best deadlift calculation
- Total calculation
- Handling failed attempts
- Handling missing attempts
- Attempt validation

### Service Tests

Service tests should verify that each service correctly handles its own responsibilities.

Examples:

- Athlete Service creates athletes correctly.
- Attempt Service rejects invalid attempt numbers.
- Attempt Service retrieves all attempts for an athlete.
- Scoring Service calculates totals correctly.

### Integration Tests

Integration tests should verify complete flows.

Important integration flow:

1. Create meet.
2. Create athlete.
3. Add attempts.
4. Mark attempts as good/no lift.
5. Request leaderboard.
6. Confirm ranking and totals are correct.

---

## 16. Logging and Observability

Each service should include basic structured logging.

Important events to log:

- Service startup
- Request received
- gRPC call started
- gRPC call failed
- Athlete created
- Meet created
- Attempt submitted
- Leaderboard calculated

Useful later additions:

- Correlation IDs
- Health checks
- Request duration logging
- Centralized logs
- Metrics dashboard

---

## 17. Local Development Plan

The project should be runnable locally.

Initial local setup:

- Run backend services locally.
- Run React frontend locally.
- Use a local database.
- Use manual API testing.

Later local setup:

- Use Docker Compose to start all services.
- Add health checks.
- Add local service discovery if needed.
- Add a seed data script or sample meet data.

---

## 18. 10-Week Project Plan

## Week 1 — Project Setup and Domain Planning

### Main Goal

Understand the domain and set up the project structure.

### Tasks

- Define the core user flows.
- Decide the first version of the services.
- Create a high-level architecture diagram.
- Set up GitHub repository.
- Create folders for backend services, frontend, docs, and tests.
- Write the initial README.

### Deliverable

A clean repo with:

- Project overview
- Architecture sketch
- Planned services
- 10-week roadmap

No working app is required yet.

---

## Week 2 — Build the Single-Service Prototype

### Main Goal

Build a simple version of the backend before splitting into microservices.

### Why

This helps you understand the domain before adding distributed system complexity.

### Tasks

- Create one backend service called `IronLedger.Api`.
- Add basic domain models conceptually:
  - Athlete
  - Meet
  - Attempt
  - Leaderboard Entry
- Add basic REST endpoints conceptually:
  - Create athlete
  - List athletes
  - Create meet
  - Add attempt
  - Get leaderboard
- Use in-memory storage or a simple local database.
- Test the basic flow manually.

### Deliverable

A working single-backend version where you can:

1. Add athletes.
2. Add attempts.
3. Calculate totals.
4. View a leaderboard.

---

## Week 3 — Design gRPC Contracts

### Main Goal

Define the gRPC service boundaries before splitting the app.

### Tasks

- Decide what each service owns.
- Design `.proto` contracts on paper first.
- Define request and response shapes for:
  - `AthleteService`
  - `MeetService`
  - `AttemptService`
  - `ScoringService`
  - `LeaderboardService`
- Decide common ID format.
- Decide error-handling conventions.
- Decide which service calls which other service.

### Deliverable

A contract design document that answers:

- What services exist?
- What operations does each service expose?
- What data does each service own?
- What data does each service need from other services?

---

## Week 4 — Split Out Athlete and Meet Services

### Main Goal

Create the first two real gRPC services.

### Tasks

- Create `IronLedger.AthleteService`.
- Create `IronLedger.MeetService`.
- Move athlete-related behavior into `AthleteService`.
- Move meet-related behavior into `MeetService`.
- Create `IronLedger.ApiGateway`.
- Have the API Gateway call the gRPC services internally.
- Keep React out of the project for now.

### Deliverable

A backend where client requests go through the API Gateway, then into `AthleteService` and `MeetService` through gRPC.

You should be able to create and list athletes and meets through the gateway.

---

## Week 5 — Add Attempt Service

### Main Goal

Build the service that tracks lifts.

### Tasks

- Create `IronLedger.AttemptService`.
- Add attempt tracking for:
  - Squat
  - Bench
  - Deadlift
- Support:
  - Attempt number 1, 2, and 3
  - Weight
  - Result: pending, good lift, no lift
- Add calls to retrieve:
  - All attempts for an athlete
  - All attempts for a meet
- Update the API Gateway so external clients can submit attempts.

### Deliverable

A system where you can:

1. Register an athlete.
2. Create a meet.
3. Submit squat, bench, and deadlift attempts.
4. Retrieve attempts through the gateway.

---

## Week 6 — Add Scoring and Leaderboard Services

### Main Goal

Add cross-service orchestration.

### Tasks

- Create `IronLedger.ScoringService`.
- Create `IronLedger.LeaderboardService`.
- `ScoringService` calculates:
  - Best squat
  - Best bench
  - Best deadlift
  - Total
- `LeaderboardService` builds rankings using:
  - Athlete data
  - Attempt data
  - Scoring results
- Add leaderboard endpoints to the API Gateway.
- Decide how to handle lifters with missing attempts.

### Deliverable

A working leaderboard generated from multiple services.

This is the first week where the project really starts to feel like a microservice system.

---

## Week 7 — Add React Frontend

### Main Goal

Add a small dashboard without making React the main project.

### Tasks

Create a React app with simple pages:

- Meets page
- Athletes page
- Add athlete form
- Attempt entry page
- Leaderboard page

React should call only the API Gateway.

It should not call gRPC services directly.

### Deliverable

A simple browser UI that can:

1. View athletes.
2. Add athletes.
3. Enter attempts.
4. View leaderboard.

Keep the UI plain. Functionality matters more than styling.

---

## Week 8 — Add Persistence and Validation

### Main Goal

Move from temporary data to persistent storage.

### Tasks

- Add a database.
- Start with SQLite or PostgreSQL.
- Decide whether each service gets its own database.
- Add validation rules:
  - Attempt weight must be positive.
  - Attempt number must be 1, 2, or 3.
  - Lift type must be squat, bench, or deadlift.
  - Result must be valid.
  - Athlete must exist before attempts are entered.
- Add better error messages through the gateway.

### Deliverable

A version where data survives restarts and invalid requests are handled cleanly.

---

## Week 9 — Testing, Logging, and Debugging

### Main Goal

Make the project feel closer to a real team environment.

### Tasks

- Add unit tests for scoring logic.
- Add service-level tests for important gRPC operations.
- Add integration tests for the main flow:
  - Create athlete
  - Create meet
  - Submit attempts
  - Calculate leaderboard
- Add structured logging.
- Add request IDs or correlation IDs.
- Add health checks for services.
- Practice debugging a failed gRPC call.

### Deliverable

A more reliable backend with tests and useful logs.

---

## Week 10 — Polish, Documentation, and Demo

### Main Goal

Turn the project into something you can explain in interviews or to your new team.

### Tasks

- Clean up README.
- Add architecture diagram.
- Add service ownership table.
- Add local setup instructions.
- Add screenshots of the React dashboard.
- Write a short “What I learned” section.
- Write a short “Future improvements” section.
- Prepare a 3–5 minute demo flow.

### Deliverable

A polished portfolio-style project.

Demo flow:

1. Create a meet.
2. Add athletes.
3. Enter attempts.
4. Mark attempts good/no lift.
5. View leaderboard.
6. Explain how services communicate using gRPC.

---

## 19. Project Milestones

### Milestone 1: Domain Prototype

A single backend can create athletes, create meets, submit attempts, and show a leaderboard.

### Milestone 2: gRPC Service Split

Athlete, Meet, Attempt, Scoring, and Leaderboard services exist and communicate through gRPC.

### Milestone 3: React Dashboard

A simple React frontend can manage the core meet flow through the API Gateway.

### Milestone 4: Persistence and Validation

Data survives restarts and invalid requests are handled cleanly.

### Milestone 5: Portfolio-Ready Project

The project has tests, logging, documentation, diagrams, and a clear demo.

---

## 20. Stretch Goals

Possible future improvements:

- Authentication and user roles
- Meet director login
- Public leaderboard page
- Real-time leaderboard updates
- Referee light system
- Federation-specific rules
- DOTS or IPF GL scoring
- Team scoring
- Import athletes from CSV
- Export results to CSV
- Docker Compose setup
- Cloud deployment
- CI/CD pipeline
- Message queue for event-driven updates

---

## 21. Suggested Repository Structure

Suggested high-level structure:

- `backend/`
  - `IronLedger.ApiGateway/`
  - `IronLedger.AthleteService/`
  - `IronLedger.MeetService/`
  - `IronLedger.AttemptService/`
  - `IronLedger.ScoringService/`
  - `IronLedger.LeaderboardService/`
  - `IronLedger.Shared/`
- `frontend/`
  - `iron-ledger-client/`
- `tests/`
  - `IronLedger.UnitTests/`
  - `IronLedger.IntegrationTests/`
- `docs/`
  - `architecture.md`
  - `grpc-contracts.md`
  - `roadmap.md`
- `README.md`

---

## 22. Final Success Criteria

The project is successful if it demonstrates that the developer can:

- Design a multi-service .NET application
- Define clear service boundaries
- Use gRPC for backend communication
- Build an API Gateway for frontend access
- Create a simple React dashboard
- Model a real domain
- Handle validation and errors
- Write meaningful tests
- Debug service-to-service communication
- Explain the architecture clearly

---

## 23. Recommended Build Order

Do not start with all five services immediately.

Recommended order:

1. Single backend prototype.
2. Split out `AthleteService`.
3. Split out `AttemptService`.
4. Add `ScoringService`.
5. Add `LeaderboardService`.
6. Add React dashboard.

This keeps the project from becoming too abstract too early.
