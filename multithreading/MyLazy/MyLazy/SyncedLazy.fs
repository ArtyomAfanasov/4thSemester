module SyncedLazy

    open ILazy
    open System.Threading

    /// Многопоточная реализация для lazy-объекта с синхронизацией.
    type SyncedLazy<'a> (supplier : unit -> 'a) =       
        /// Результат вычисления.
        [<DefaultValue>] val mutable result : 'a
        
        /// Выполнено ли вычисление.
        let isValueCreated = ref false

        /// Объект для синхронизации вычисления. 
        let locker = new System.Object()

        interface ILazy<'a> with
            member this.Get () =
                if !isValueCreated then this.result
                else
                    lock locker (fun () -> 
                        if !isValueCreated then this.result
                        else                 
                            this.result <- (supplier ())
                            Volatile.Write(isValueCreated, true)
                            
                            // JIT should not move next instruction before calling calculation, should it? Threrefore we do not need Volatile.Write here.
                            this.result)                   
                            
        /// Выполнено ли вычисление.
        member this.IsValueCreated = isValueCreated