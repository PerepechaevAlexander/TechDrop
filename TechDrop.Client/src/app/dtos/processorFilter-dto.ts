// Фильтры для процессоров
export interface ProcessorFilterDto{
  manufacturers: string[],
  available: boolean,
  minCost: number|null,
  maxCost: number|null
}
