import { UserInfo } from "../features/auth/types";

export type UserContext = {
  userInfo: UserInfo | undefined;
};

export type PageData<T> = UserContext & T;
