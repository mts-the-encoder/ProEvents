import { SocialMedia } from './SocialMedia';

export interface Speaker {
  id: number;
  name: string;
  curriculum: string;
  imageURL: string;
  phone: string;
  email: string;
  socialMedias: SocialMedia[];
  eventsSpeakers: Speaker[];
}
