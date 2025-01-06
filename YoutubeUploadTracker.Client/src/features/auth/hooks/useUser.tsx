import { usePageContext } from "vike-react/usePageContext";
import { PageContext } from "vike/types";
import { UserInfo } from "../types";

type UserPageContext = PageContext<{
  userInfo: UserInfo | undefined;
}>;

export default function useUser() {
  const pageContext = usePageContext() as UserPageContext;

  return pageContext?.data?.userInfo;
}
