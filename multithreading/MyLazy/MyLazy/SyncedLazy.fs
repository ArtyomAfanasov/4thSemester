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
        let lockObj = new System.Object()

        /// Монитор для синхронизации вычисления. 
        let lock (lockObj : obj) f =
            Monitor.Enter lockObj
            try 
                f ()
            finally
                Monitor.Exit lockObj

        interface ILazy<'a> with
            member this.Get () =
                if !isValueCreated then this.result
                else
                    lock lockObj (fun () -> 
                        if !isValueCreated then this.result
                        else                 
                            this.result <- (supplier ())    
                            Volatile.Write(isValueCreated, true)
                            this.result)                   
                            
        /// Выполнено ли вычисление.
        member this.IsValueCreated = isValueCreated