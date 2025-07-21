# **MDSYS-Local Message Notifier**

**MDSYS-Local Message Notifier** is a powerful and intuitive desktop utility for Windows, designed for system administrators and power users who need to send real-time notifications to user sessions on a local machine. Built with a modern .NET architecture, this tool provides a reliable way to communicate system alerts, maintenance warnings, and other important messages.

## **Features**

* **Dynamic User Session Discovery:** Automatically detects and displays a list of all user sessions on the machine.  
* **Real-Time Updates:** The user list refreshes automatically when users log on or off, ensuring you always have an up-to-date view.  
* **Advanced Status Filtering:** Filter the user list by one or more connection states (Active, Disconnected, Idle, etc.) to precisely target your audience.  
* **Flexible Messaging Options:**  
  * Send messages to a single, specific user.  
  * Broadcast messages to all users matching your current filter.  
  * Multi-select a custom group of users to notify.  
* **Cancellable Operations:** Long-running send operations can be canceled midway through.  
* **Message Template Manager:** Create, save, and reuse message templates for common announcements, saving time and effort.  
* **User-Friendly Interface:**  
  * Clean and intuitive layout.  
  * Non-blocking UI keeps the application responsive.  
  * Status bar provides instant feedback on send operations.  
  * Support for Right-to-Left (RTL) text in messages.

## **Requirements**

* **Operating System:** Windows 10 / Windows Server 2016 or later.  
* **Framework:** .NET 8 (or the version it's compiled with).  
* **Permissions:** The application requires **Administrator Privileges** to query user sessions and send messages. It will automatically prompt for elevation via the User Account Control (UAC) dialog when launched.

## **How to Use**

1. Download the latest release from the [Releases](https://github.com/mohanad-cs/LocalOSUserNotifier/tree/main/LocalOSUserNotifier/ToolRelease)) Folder.  
2. install or Upadate the Tool  
3. Run MDSYS-LocalMessageNotifier.exe.  
4. The application will request administrator permissions. Please accept the UAC prompt.  
5. Use the filter checkboxes to select which user statuses you wish to view.  
6. Select the target users or choose "Send to All".  
7. Type your message or select one from a template.  
8. Click "Send".

## **Architecture**

This tool is built using modern software design principles to ensure it is robust, maintainable, and scalable.

* **Separation of Concerns:** The solution is divided into two main projects:  
  * MDSYS.LocalMessageNotifier.Core: A class library containing all business logic, services, and data models. It has no dependency on the UI.  
  * MDSYS.LocalMessageNotifier.UI: The Windows Forms project, responsible only for the user interface.  
* **Dependency Injection (DI):** Services are registered and resolved using the Microsoft.Extensions.DependencyInjection container, promoting a decoupled and testable codebase.  
* **Asynchronous Operations:** All potentially long-running operations (like sending messages and file I/O) are fully async, ensuring the UI remains responsive at all times.

## **Author**

Developed by **MDSYS-Mohanad Shamsan**.
