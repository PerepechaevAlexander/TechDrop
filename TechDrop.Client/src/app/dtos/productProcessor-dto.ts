import {Picture} from "./picture-dto";
import {GraphCore} from "./graphCore-dto";

export interface ProductProcessor {
  productId: number,
  description: string,
  cost: number,
  quantity: number,
  discount: number,
  pictures: Picture[],
  manufacturer: string,
  processorId: number,
  model: string,
  socket: string,
  year: number,
  coolingSystem: boolean,
  cores: number,
  threads: number,
  performanceCores: number,
  energyCores: number,
  l2: number,
  l3: number,
  techProcess: number,
  baseFrequency: number,
  maxFrequency: number,
  baseFrequencyEnergyCores: number,
  maxFrequencyEnergyCores: number,
  freeMultiplier: boolean,
  ramTypes: string[],
  ramCapacity: number,
  ramChannels: number,
  ramMaxFrequency: number,
  tdp: number,
  maxTemp: number,
  graphCoreAvailable: boolean,
  graphCore: GraphCore,
  pciExpress: string,
  pciExpressLines: number
}
