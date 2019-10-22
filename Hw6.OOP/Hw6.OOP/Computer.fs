/// Содержит информацию о компьютере.
module Computer

open OS

/// Класс с информацией о компьютере.
type Computer(name: string, OS: OS, infected: bool) =
    /// Имя компьютера.
    member val Name = name with get, set

    /// ОС компьютера.
    member val OS = OS with get, set

    /// Здоровье компьютера.
    member val Infected = infected with get, set