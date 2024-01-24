import {Picture} from "./picture-dto";

export interface CatalogProcessor {
  productId: number,
  cost: number,
  quantity: number,
  discount: number,
  pictures: Picture[],
  manufacturer: string,
  processorId: number,
  model: string,
  cores: number,
  baseFrequency: number,
  maxFrequency: number,
  techProcess: number,
  graphCoreModel: string,
  tdp: number
}
