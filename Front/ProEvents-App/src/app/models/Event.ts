import { SocialMedia } from './SocialMedia';
import { Lot } from './Lot';
import { Speaker } from './Speaker';

export interface Event {
  id: number;
  local: string;
  eventDate?: Date
  theme: string;
  qtdPeople: number
  imageURL: string;
  phone: string;
  email: string;
  lots?: Lot[];
  socialMedias?: SocialMedia[];
  eventsSpeakers?: Speaker[];
}
