# Profanity Filter Chat App

## Overview
The **Profanity Filter Chat App** is a simple chat application designed to filter and block profane words in real-time. This project is developed to demonstrate how content filtering can be implemented in a desktop application using C# and WPF.

## Features
- Real-time profanity filtering
- Easy-to-use interface
- Customizable list of profane words (stored in a JSON file)
- Lightweight and efficient

## Installation

### Prerequisites
- Windows OS
- .NET Framework 4.8

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/kouridev/profanity-filter-chat-app.git
   ```
2. Open the solution file (`chat-app.sln`) in Visual Studio.
3. Build the project to restore dependencies and compile the code.
4. Run the application by pressing `F5` or navigating to the `bin/Debug` folder and executing `chat-app.exe`.

## Usage
1. Launch the application.
2. Enter your messages in the chat input field.
3. Messages containing profane words will be automatically filtered.

## Configuration
- You can customize the list of profane words by editing the `profaneWords.json` file located in the application directory.

## Technologies Used
- **C#**: Primary programming language
- **WPF (Windows Presentation Foundation)**: UI framework
- **Newtonsoft.Json**: For handling JSON files

## Contributing
Contributions are welcome! Follow these steps to contribute:
1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your message here"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Open a pull request.

## License
This project is licensed under the [MIT License](LICENSE).

## Contact
For questions or feedback, please reach out via the repository's issues section or contact me directly at **kouri@example.com**.
