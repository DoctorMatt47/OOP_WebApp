import {Question} from "./question.model";

export class Test {
  id?: string;
  title?: string;
  description?: string;
  questions: Array<Question> = new Array<Question>();
  username?: string;
}
