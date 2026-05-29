# CyberBot-Part-Two

# CyberBot Part Two README

## Project Title

CyberBot Part Two – WPF Cybersecurity Awareness Chatbot

---

# Introduction

CyberBot Part Two is an improved cybersecurity awareness chatbot developed using:

* C#
* WPF (Windows Presentation Foundation)
* XAML

The application was upgraded from a console application into a modern graphical chatbot interface.

CyberBot helps users learn about cybersecurity topics such as:

* Phishing
* Password Security
* VPNs
* Firewalls
* Fraud
* Hacked Accounts
* Safe Browsing

The chatbot also includes memory, sentiment detection, and personalised responses.

---

# Objectives of Part Two

The main goals of Part Two were:

* Create a graphical user interface using WPF
* Improve user interaction
* Add memory and recall functionality
* Add sentiment detection
* Store user information using text files
* Improve chatbot responses
* Create a more realistic chatbot experience

---

# Technologies Used

| Technology    | Purpose           |
| ------------- | ----------------- |
| C#            | Application Logic |
| WPF           | User Interface    |
| XAML          | GUI Design        |
| .NET          | Framework         |
| File Handling | Data Storage      |

---

# Application Structure

The project is divided into multiple classes for better organisation.

| File/Class           | Purpose                  |
| -------------------- | ------------------------ |
| `MainWindow.xaml`    | GUI Design               |
| `MainWindow.xaml.cs` | Main chatbot logic       |
| `respond.cs`         | Stores chatbot responses |
| `user_name.cs`       | Handles usernames        |
| `sentiment.cs`       | Detects emotions         |
| `voice_greeting.cs`  | Plays greeting sound     |

---

# Features Implemented

# 1. WPF Graphical User Interface

Part Two introduced a modern GUI using WPF.

The interface includes:

* Home Screen
* Username Screen
* Chat Screen
* Chat message display
* Buttons and textboxes
* Large chatbot logo

### GUI Grids

| Grid            | Purpose           |
| --------------- | ----------------- |
| `home_grid`     | Welcome screen    |
| `username_grid` | Username entry    |
| `chat_grid`     | Chat conversation |

---

# 2. Custom Background and Logo

The application design was improved with:

* Cyan background
* Large CyberBot logo
* Styled buttons
* Organised layout

### Example

```xml
Background="Cyan"
```

---

# 3. Voice Greeting

When the application starts, a greeting sound is played.

### Class Used

```csharp
voice_greeting.cs
```

### Method Used

```csharp
PlaySound()
```

### Features

* Uses `SoundPlayer`
* Plays `greeting.wav`
* Improves user experience

---

# 4. Username Handling

The chatbot allows users to enter their username.

### Features

* Saves usernames
* Detects returning users
* Displays personalised welcome messages

### Storage File

```text
user_names.txt
```

### Example

```text
CyberBot: Hi Norman, nice to have back again.
```

---

# 5. Memory and Recall Feature

The chatbot remembers user interests.

### Example

User:

```text
I am interested in privacy
```

Chatbot:

```text
CyberBot: Great! I will remember that you are interested in privacy.
```

### Stored File

```text
interested_topic.txt
```

### Example Saved Data

```text
Norman interested in: privacy, phishing
```

---

# 6. Automatic Recall Responses

The chatbot automatically recalls stored interests later in the conversation.

### Example

```text
CyberBot: As someone interested in privacy, you should regularly review your account security settings.
```

### Method Used

```csharp
auto_show_interest()
```

---

# 7. Sentiment Detection

The chatbot can detect user emotions.

### Supported Emotions

* Worried
* Frustrated
* Angry
* Sad
* Happy
* Confused

### Class Used

```csharp
sentiment.cs
```

### Example

User:

```text
I am worried about scams
```

Chatbot:

```text
CyberBot: It's understandable to feel worried about online threats.
Always avoid suspicious links and protect your personal information.
```

---

# 8. Automatic Cybersecurity Advice

After detecting emotions, the chatbot automatically continues with cybersecurity advice.

### Example

User:

```text
I am frustrated about phishing attacks
```

Chatbot:

```text
CyberBot: I understand you're frustrated.
Cybersecurity issues can be stressful, but I will help you step by step.

CyberBot: Phishing is a scam where attackers pretend to be trusted sources to steal information.
```

This improves conversation flow and prevents the user from repeating themselves.

---

# 9. Chat Bubble Interface

Messages are displayed using styled chat bubbles.

### WPF Components Used

* `Border`
* `TextBlock`
* `Run`

### Features

* Different colours for user and chatbot
* Improved readability
* Professional appearance

---

# 10. File Handling

The chatbot uses file handling to save data permanently.

### Files Used

| File                   | Purpose               |
| ---------------------- | --------------------- |
| `user_names.txt`       | Stores usernames      |
| `interested_topic.txt` | Stores user interests |

### File Methods Used

```csharp
File.AppendAllText()
File.ReadAllLines()
File.WriteAllLines()
```

---

# 11. Response System

Responses are stored inside:

```csharp
ArrayList
```

The chatbot searches for keywords and returns matching responses.

### Example Topics

* Cybersecurity
* Passwords
* VPNs
* Fraud
* Firewalls
* Hacked accounts

---

# 12. Ignored Words System

The chatbot ignores unnecessary words such as:

* what
* tell
* about
* me
* is

This improves keyword detection.

---

# 13. Input Validation

The chatbot validates:

* Empty messages
* Invalid input
* Special characters

### Method Used

```csharp
RemoveSpecialCharacters()
```

---

# 14. Personalized Responses

The chatbot personalises conversations using:

* Username memory
* Interest memory
* Sentiment detection

This makes interactions more realistic and engaging.

---

# Challenges Faced

Some challenges experienced during development included:

* Designing the WPF interface
* Managing multiple grids
* Implementing memory using text files
* Preventing duplicate interests
* Improving chatbot response flow
* Detecting emotions correctly

These challenges were solved through testing and debugging.

---

# Conclusion

CyberBot Part Two is a major improvement from the original console chatbot.

The application now includes:

* Modern WPF graphical interface
* Memory and recall functionality
* Sentiment detection
* Persistent file storage
* Personalised responses
* Improved cybersecurity awareness responses
* Better user interaction

The project demonstrates:

* Object-oriented programming
* GUI development
* File handling
* Event-driven programming
* User experience design
* Cybersecurity awareness education

CyberBot Part Two provides a more intelligent, interactive, and user-friendly chatbot experience.
