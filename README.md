# Iron-Ledger
Iron Ledger is a powerlifting meet management system designed as a learning project for .NET microservice architecture and gRPC communication.

The system allows meet organizers to create meets, register athletes, enter lifting attempts, calculate totals, and display leaderboards. The backend is divided into multiple .NET services that communicate through gRPC. A lightweight React frontend provides a simple administrative dashboard.

The main purpose of this project is not to build a production-ready powerlifting platform. The main purpose is to practice the structure, design, debugging, and development workflow of a .NET-based microservice system.



# Todo/Ideas:
- Judging light options: see USAPL [rulebook](https://www.usapowerlifting.com/assets/general/PDFs/USAPL-Rulebook-v2026.1-Final-with-markups.pdf) section 2.10
- Make API Calls asynchronous
- Make a Leaderboard ( Add Dots, Overall total, Weightclass Total, Gender Total, Weightclass Dot, etc) see https://www.openpowerlifting.org/rankings/raw
- Add age classes and make enums for weight and age classes 
- Should there be federation classes and if so should those have meet templates
  - Meets should have a list of valid weight classes that athletes need to be in
    - there needs to be a validation for athletes signing up


## Current Architecture Goal
```
                     ┌──────────────────┐
                     │  React Frontend  │
                     └────────┬─────────┘
                              │ HTTP/JSON
                              ▼
                     ┌──────────────────┐
                     │ API Gateway  │
                     └────────┬─────────┘
                              │ gRPC
          ┌───────────────────┼───────────────────┐
          ▼                   ▼                   ▼
  AthleteService       MeetService        AttemptService
                                                   │
                                      ┌────────────┴────────────┐
                                      ▼                         ▼
                              ScoringService          LeaderboardService
```
