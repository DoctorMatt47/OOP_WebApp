import {Option} from "./option.model";

export class Question {
  id?: string;
  string?: string;
  options: Array<Option> = new Array<Option>();
}
