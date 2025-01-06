import { PageContext } from "vike/types";
import { fetchUserDetails } from "../features/auth/service/auth";
import { UserContext } from "./types";

export type LayoutPageContext = PageContext<UserContext>;

export async function onBeforeRender(pageContext: PageContext) {
  const userInfo = await fetchUserDetails(pageContext.headers!["cookie"]!);

  return {
    pageContext: {
      data: {
        ...(pageContext.data as any),
        userInfo,
      },
    },
  };
}
