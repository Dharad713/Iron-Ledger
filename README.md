# Iron-Ledger
Iron Ledger is a powerlifting meet management system designed as a learning project for .NET microservice architecture and gRPC communication.

The system allows meet organizers to create meets, register athletes, enter lifting attempts, calculate totals, and display leaderboards. The backend is divided into multiple .NET services that communicate through gRPC. A lightweight React frontend provides a simple administrative dashboard.

The main purpose of this project is not to build a production-ready powerlifting platform. The main purpose is to practice the structure, design, debugging, and development workflow of a .NET-based microservice system.



# Todo/Ideas:
- Judging light options: see USAPL [rulebook](https://www.usapowerlifting.com/assets/general/PDFs/USAPL-Rulebook-v2026.1-Final-with-markups.pdf) section 2.10
- Make API Calls asynchronous
- Make a Leaderboard ( Add Dots, Overall total, Weightclass Total, Gender Total, Weightclass Dot, etc
