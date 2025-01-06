import { backendUrl } from "../../common/utils/urls";

export const triggerChannelImport = async () => {
  await fetch(`${backendUrl}/channels/import`, {
    method: "POST",
  });
};
