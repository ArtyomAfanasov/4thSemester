module OS

/// Абстрактный класс с данными о сопротивляемости вирусам разных ОС.
type IResistance =       
    abstract member LinuxResistance: int
    abstract member MacOSResistance: int
    abstract member WindowsResistance: int
    abstract member OtherOSResistance: int

/// Стандартная сопротивляемость вирусам.
type DefaultResistance() =
    interface IResistance with
        member this.LinuxResistance = 70
        member this.MacOSResistance = 60
        member this.WindowsResistance = 30
        member this.OtherOSResistance = 75

/// Операционные системы.
type OS =
    | Linux
    | Windows
    | MacOS
    | Other