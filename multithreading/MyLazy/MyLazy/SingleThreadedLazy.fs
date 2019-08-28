/// Содержит однопоточную реализацию lazy-объекта.
module SingleThreadedLazy
    
    open ILazy

    /// Однопоточная реализация для lazy-объекта.
    type SingleThreadedLazy<'a> (supplier : unit -> 'a) =       
        /// Результат вычисления.
        [<DefaultValue>] val mutable result : 'a
        /// Выполнено ли вычисление.
        let mutable isCalculated = false

        interface ILazy<'a> with
            member this.Get () =
                if isCalculated then this.result
                else                 
                    this.result <- (supplier ())    
                    isCalculated <- true
                    this.result