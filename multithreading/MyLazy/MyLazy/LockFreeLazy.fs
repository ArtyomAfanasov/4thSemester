module LockFreeLazy
    
    open ILazy
    open System.Threading

    /// Многопоточная реализация для lock-free lazy-объекта.
    type LockFreeLazy<'a> (supplier : unit -> 'a) =                       
        /// Результат вычисления.
        let mutable result = None
    
        /// Выполнено ли вычисление.
        let isValueCreated = ref false        

        interface ILazy<'a> with
            member this.Get () =
                if !isValueCreated then Option.get result
                else
                    let newResult = Some(supplier())
                    Interlocked.CompareExchange(&result, newResult, None) |> ignore
                    Volatile.Write(isValueCreated, true)
                    
                    Option.get result

        /// Выполнено ли вычисление.
        member this.IsValueCreated = isValueCreated