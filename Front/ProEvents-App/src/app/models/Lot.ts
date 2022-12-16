import { Event } from './Event';

export interface Lot {
  id: number;
  name: string;
  price: string;
  startDate?: Date;
  endDate?: Date;
  qtd: number;
  eventId: number;
  event: Event;
}
