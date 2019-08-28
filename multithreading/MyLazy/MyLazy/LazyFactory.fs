/// Создание lazy-объектов различной реализации.
module LazyFactory

    open SingleThreadedLazy
    open SyncedMultithreadingLazy
    open LockFreeMultithreadingLazy

    /// Фабрика lazy-объектов.
    type Factory() =
        /// Однопоточная реализация lazy-объекта.
        static member CreateSingleThreadedLazy (supplier : unit -> 'a) = new SingleThreadedLazy<'a>(supplier)

        /// Многопоточная реализация lazy-объекта с синхронизацией.
        static member CreateSyncedMultithreadingLazy (supplier : unit -> 'a) = new SyncedMultithreadingLazy<'a>(supplier)

        /// Многопоточная реализация lazy-объекта в lock-free стиле.
        //static member CreateLockFreeMultithreadingLazy supplier = new LockFreeMultithreadingLazy(supplier)

    [<EntryPoint>]
    let main a =
        0