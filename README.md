# **Echoes of Time**

A **time tracking application** designed for task management and accurate time tracking, built using WPF (Windows Presentation Foundation) with the MVVM architectural pattern.

## **Overview**

**Echoes of Time** is a desktop application that allows users to track time spent on tasks efficiently. It supports managing tasks for specific dates, tracking elapsed time for active tasks, and manually adjusting task durations.

### **Key Features**
- **Task Management**:
  - Add, delete, and manage tasks for specific dates.
- **Time Tracking**:
  - Activate a task to track elapsed time automatically.
  - Manually increment or decrement task time.
- **Single Active Task**:
  - Ensures only one task can be active at a time.
- **Persistent Storage**:
  - Uses SQLite for saving and retrieving tasks.
- **User-Friendly UI**:
  - Buttons and icons with intuitive Unicode symbols for actions:
    - ➕ Add Task
    - ➖ Decrement Time
    - ➕ Increment Time
    - 🗑 Delete Task
    - 🎯 Go to Active Task
- **Dynamic Date Navigation**:
  - Navigate tasks for specific dates using Previous and Next buttons.
  - View tasks for the active date with a "Go to Active Task" button.

---

## **Technologies Used**
- **.NET 8** (Windows-only WPF Application)
- **MVVM (Model-View-ViewModel)** architectural pattern
- **SQLite** database for task persistence
- **XAML** for defining UI components

---

## **Getting Started**

### **Prerequisites**
- **Windows OS**: Required for running WPF applications.
- **.NET 8 SDK**: Install the .NET 8 SDK from [dotnet.microsoft.com](https://dotnet.microsoft.com/).

### **Clone the Repository**
```bash
git clone <repository-url>
cd EchoesOfTime
```

---

### **Build and Run**
1. Open the solution in Visual Studio 2022 or later.
2. Restore NuGet packages:
	```bash
	dotnet restore
	```
3. Build the project:
	```bash
	dotnet build
	```
4. Run the application:
	```bash
	dotnet run
	```

---

## **Usage**

1. **Add a Task**:
   - Click the **➕ Add Task** button to create a new task for the selected date.
2. **Activate/Deactivate a Task**:
   - Click the **▶/◼** button to start or stop tracking time for a task.
3. **Navigate Dates**:
   - Use the **◀ Previous Day** and **▶ Next Day** buttons to view tasks for other dates.
   - Use the **🎯 Go to Active Task** button to navigate to the active task's date.
4. **Manage Time**:
   - Use the **➕ Increment Time** and **➖ Decrement Time** buttons to adjust elapsed time manually.
5. **Delete a Task**:
   - Click **🗑 Delete Task** to remove a task.

---

## **Database Configuration**

The application uses SQLite for storing tasks persistently. The database schema is automatically generated using Entity Framework Core migrations.

### **Database Schema**
The `Tasks` table includes the following fields:
- **Id**: Primary key.
- **Name**: Name of the task.
- **ElapsedTime**: Total time tracked for the task.
- **ActiveSince**: Nullable datetime to track when the task was last activated.
- **Details**: Optional details or description of the task.
- **Date**: The date the task belongs to.

---

## **Contributing**

Contributions are welcome! If you'd like to contribute:
1. Fork the repository.
2. Create a new branch for your feature or fix.
3. Submit a pull request describing your changes.

---

## **License**

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

---

## **Acknowledgments**
- Inspired by my original time tracking application I created, as a more junior developer back in 2013, using .NET 4.0, Windows Forms, and XML file storage.
- Built with WPF to explore the flexibility and power of MVVM in desktop applications.
