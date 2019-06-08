/// Модуль для задачи:
/// Записать в point-free стиле func x l = List.map (fun y -> y * x) l. 
/// Выписать шаги вывода и проверить с помощью FsCheck корректность результата
module PointFree

/// Запись функции func x l = List.map (fun y -> y * x) l
/// в point-free стиле.

/// Вывод:
/// func x l = List.map (fun y -> y * x) l
/// func x l = List.map ((*) x) l
/// 
///
///
let pointFree =
    ()