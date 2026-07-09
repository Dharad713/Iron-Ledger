# Goal: Define what each services should and should not do

## Overall flow:
```
CreateAthlete
↓
RegisterAthleteForMeet
↓
AddAttempts
↓
RecordAttemptResults
↓
DetermineBestLiftsAndTotal
↓
CalculateScore
↓
BuildLeaderboard
```

## My services:

**AthleteService**:
- **Owns:** `Athlete`
  - Each Athlete has a global Guid, and can compete in more than one meet
- **Responsibilities:** 
  - Create athlete
    - Validate athlete's data
    - Make a new athlete id
    - Persist the athlete (add to db)
    - Return the created athlete
  - Get athlete
  - List athletes
  - Update athlete
  - Archive athlete
  - Restore athlete
- **Potential Responsibilities**
  - Search athletes by name
  - Get athletes by IDs
  - Manage athlete profile information
  - Manage nationality
  - Manage team affiliation


**MeetService**:
- MeetService is the source of truth for Meet and MeetRegistration data.
- **Owns**: 
  - `Meet`
    - Each meet has a global Guid
  - `Meet Registration`
    - Adds an `Athlete` to a meet
    - Probably does not need to be a separate service
  - **Responsibilities**:
    - Meet Management:
      - CreateMeet
      - GetMeet
      - ListMeets
      - UpdateMeet
      - CancelMeet
    - Registration:
      - RegisterAthlete
      - WithdrawAthlete
      - GetRegistration
      - ListRegistrations
      - UpdateRegistration

- **Lifecycle:** (allows for things to only be able to happen in a specific order)
  - OpenRegistration
  - CloseRegistration
  - StartMeet
  - CompleteMeet
  - CancelMeet


- **Potential Responsibilities**
  - Register athlete for meet
  - Withdraw athlete from meet
  - List registered athletes
  - Update athlete registration
  - Assign weight class
  - Assign division
  - Assign flight


**AttemptService**:
- Owns: `Attempt` and can calculate competiton results (total)
- Responsibilities:
    - Create attempt
    - RecordAttemptResult
    - Get attempts for a meet
    - Get attempt for an athlete
    - Update attempt result
    - Delete attempt result
    - Attempt service verifies AthleteId and MeetId
    - Calculate Total
- Limitations/Notes:
  - Gets SOT (source of truth) from Meet and Athlete
    - Looks up meetId and athleteId
  - Attempts reference an athlete by AthleteId, but AttemptService owns attempt data

**LeaderboardService**:
- Owns: No data initially, since it creates the table on request
  - Uses athlete id to index
- Responsibilities:
  - Generate meet leaderboard
  - Generate global leaderboard
  - Filter leaderboard
  - Rank results
  - Select scoring system
- Potential Responsibilities
  - Could potentially have a persistent global leaderbaord

**ScoringService**:

- Converts `bodyWeight`, `sex`, `totalKg` into DOTS and other scoring systems
  - Will use a `CalculateDots` or `CalculateWilks` call
- Input:
  - BodyweightKg
  - Sex
  - TotalKg
  - Formula
- Output:
  - Score
  - Formula
  - Year (like dots 2020 )


